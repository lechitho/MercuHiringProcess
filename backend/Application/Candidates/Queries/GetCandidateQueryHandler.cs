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

public sealed record GetCandidatesQuery() : IQuery<IEnumerable<Candidate>>;

public sealed class GetCandidatesQueryHandler : IQueryHandler<GetCandidatesQuery, IEnumerable<Candidate>>
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly ICacheService _cacheService;
    public GetCandidatesQueryHandler(ICandidateRepository candidateRepository, ICacheService cacheService)
    {
        _candidateRepository = candidateRepository;
        _cacheService = cacheService;
    }

    public async Task<Result<IEnumerable<Candidate>>> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
    {
        var cacheKey = $"GetCandidatesQuery";

        var candidates = await _cacheService.GetOrCreateAsync(cacheKey, async () => await _candidateRepository.GetAsync());

        return new Result<IEnumerable<Candidate>>(candidates);
    }
}
