using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Candidates.Commands;

public sealed record UpdateCandidateCommand(Guid candidateId, Candidate Candidate) : ICommand<Candidate>;
public sealed class UpdateCandidateCommandHandler : ICommandHandler<UpdateCandidateCommand, Candidate>
{
    private readonly ICandidateRepository _candidateRepository;
    public UpdateCandidateCommandHandler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<Result<Candidate>> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
    {
        var updatedCandidate = Candidate.Create(request.candidateId, request.Candidate.Name, request.Candidate.Stage, request.Candidate.Phone, request.Candidate.Email);

        var success = await _candidateRepository.UpdateAsync(updatedCandidate);

        return success ? Result<Candidate>.Success(updatedCandidate) : Result<Candidate>.Failure(new Error("Candidate.NotFound", $"Could not find candidate {request.candidateId} to update"));
    }
}
