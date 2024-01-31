using Application.Abstractions.Caching;
using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Candidates.Queries;

public sealed record GetCandidateByIdQuery(Guid CandidateId) : IQuery<Candidate>;
public sealed class GetCandidateByIdQueryHandler : IQueryHandler<GetCandidateByIdQuery, Candidate>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ICacheService _cacheService;
    public GetCandidateByIdQueryHandler(ICandidateRepository candidateRepository, ICacheService cacheService)
    {
        _candidateRepository = candidateRepository;
        _cacheService = cacheService;
    }

    public async Task<Result<Candidate>> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"GetCandidateById_{request.CandidateId}";

        var candidate = await _cacheService.GetOrCreateAsync(cacheKey, async () => await _candidateRepository.GetByIdAsync(request.CandidateId));

        if (candidate is null)
            return Result<Candidate>.Failure(new Error("Candidate.NotFound", $"The Candidate with Id {request.CandidateId} was not found"));

        return new Result<Candidate>(candidate);
    }
}
