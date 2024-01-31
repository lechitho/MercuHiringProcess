using Application.Abstractions.Messaging;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Shared;


namespace Application.Candidates.Commands;

public sealed record CreateCandidateCommand(string Name, string Stage, string Phone, string Email) : ICommand<Guid>;

public sealed class CreateCandidateCommandHandler : ICommandHandler<CreateCandidateCommand, Guid>
{
    private readonly ICandidateRepository _candidateRepository;

    public CreateCandidateCommandHandler(ICandidateRepository candidateRepository)
    {
        _candidateRepository = candidateRepository;
    }

    public async Task<Result<Guid>> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
    {
        var candidate = Candidate.Create(Guid.NewGuid(), request.Name, request.Stage, request.Phone, request.Email);

        await _candidateRepository.AddAsync(candidate);

        return Result<Guid>.Success(candidate.Id);
    }
}

