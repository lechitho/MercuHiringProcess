using Application.Candidates.Commands;
using Domain.Abstractions;
using Domain.Entities;
using Domain.Exception;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test;

public class CreateCandidateCommandHandlerTests
{
    private readonly Mock<ICandidateRepository> _candidateRepositoryMock;
    public CreateCandidateCommandHandlerTests()
    {
        _candidateRepositoryMock = new();
    }
    [Fact]
    public async Task Handler_Should_ReturnTrueResult_WhenCreateANewCandidate()
    {
        //Arrange
        var createCandidateCommand = new CreateCandidateCommand("Jony Ive","Applied", "0906556941", "JonyIve@example.com");
        var handler = new CreateCandidateCommandHandler(_candidateRepositoryMock.Object);

        //Act
        var result = await handler.Handle(createCandidateCommand, CancellationToken.None);

        //Assert
        _candidateRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Candidate>()), Times.Once);
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldCreateCandidate()
    {
        // Arrange
        var candidateRepositoryMock = new Mock<ICandidateRepository>();
        var createCandidateCommand = new CreateCandidateCommand("Jony Ive", "Applied", "0906556941", "JonyIve@example.com");
        var handler = new CreateCandidateCommandHandler(candidateRepositoryMock.Object);

        // Act
        var result = await handler.Handle(createCandidateCommand, CancellationToken.None);

        // Assert
        candidateRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Candidate>()), Times.Once);
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public async Task Handle_InvalidName_ShouldReturnThrowExceptionResult()
    {
        // Arrange
        var candidateRepositoryMock = new Mock<ICandidateRepository>();
        var createCandidateCommand = new CreateCandidateCommand("", "Applied", "0906556941", "JonyIve@example.com");
        var handler = new CreateCandidateCommandHandler(candidateRepositoryMock.Object);

        // Act
        var result = handler.Handle(createCandidateCommand, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<CandidateCannotCreatedException>(() => result);
    }

    [Fact]
    public async Task Handle_InvalidPhone_ShouldReturnThrowExceptionResult()
    {
        // Arrange
        var candidateRepositoryMock = new Mock<ICandidateRepository>();
        var createCandidateCommand = new CreateCandidateCommand("Jony Ive", "Applied", "", "JonyIve@example.com");
        var handler = new CreateCandidateCommandHandler(candidateRepositoryMock.Object);

        // Act
        var result = handler.Handle(createCandidateCommand, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<CandidateCannotCreatedException>(() => result);
    }

    [Fact]
    public async Task Handle_InvalidEmail_ShouldReturnThrowExceptionResult()
    {
        // Arrange
        var candidateRepositoryMock = new Mock<ICandidateRepository>();
        var createCandidateCommand = new CreateCandidateCommand("Jony Ive", "Applied", "0906556941", "");
        var handler = new CreateCandidateCommandHandler(candidateRepositoryMock.Object);

        // Act
        var result = handler.Handle(createCandidateCommand, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<CandidateCannotCreatedException>(() => result);
    }
}
