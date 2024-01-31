using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Candidates.Commands;

public sealed record DeleteCandidateCommand(Guid candidateId) : ICommand<Unit>;
public sealed class DeleteCandidateCommandHandler : ICommandHandler<DeleteCandidateCommand, Unit>
{
    private readonly ICandidateRepository _candidateRepository;
    public DeleteCandidateCommandHandler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<Result<Unit>> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
    {
        await _candidateRepository.RemoveAsync(request.candidateId);
        return Result<Unit>.Success(Unit.Value);
    }
}
