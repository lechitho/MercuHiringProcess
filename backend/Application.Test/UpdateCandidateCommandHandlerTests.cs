using Application.Candidates.Commands;
using Domain.Abstractions;
using Domain.Entities;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test
{
    public class UpdateCandidateCommandHandlerTests
    {
        private readonly Mock<ICandidateRepository> _candidateRepositoryMock;

        public UpdateCandidateCommandHandlerTests()
        {
            _candidateRepositoryMock = new();
        }

        [Fact]
        public async Task Handler_Should_ReturnTrueResult_WhenUpdateCandidate()
        {
            // Arrange
            var CandidateId = Guid.NewGuid();
            var updateCandidateCommand = new UpdateCandidateCommand(CandidateId, Candidate.Create(CandidateId, "Tim Cook", "Applied", "0987654321", "janesmith@example.com"));
            _candidateRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Candidate>())).ReturnsAsync(true);
            var handler = new UpdateCandidateCommandHandler(_candidateRepositoryMock.Object);

            // Act
            var result = await handler.Handle(updateCandidateCommand, CancellationToken.None);

            // Assert
            _candidateRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Candidate>()), Times.Once);
            result.IsSuccess.ShouldBeTrue();
            result.Value.ShouldNotBeNull();
            result.Value.Name.ShouldBe("Tim Cook");
        }

        [Fact]
        public async Task Handler_Should_ReturnFail_WhenUpdateANotExistedCandidate()
        {
            // Arrange
            var CandidateId = Guid.NewGuid();
            var updateCandidateCommand = new UpdateCandidateCommand(CandidateId, Candidate.Create(CandidateId, "Tim Cook", "Applied", "0987654321", "janesmith@example.com"));

            _candidateRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Candidate>())).ReturnsAsync(false);

            var handler = new UpdateCandidateCommandHandler(_candidateRepositoryMock.Object);

            // Act
            var result = await handler.Handle(updateCandidateCommand, CancellationToken.None);

            // Assert
            result.IsSuccess.ShouldBeFalse(); // Check that the result indicates failure
            result.Error.ShouldNotBeNull();
            result.Error.Message.ShouldNotBeEmpty();
        }
    }
}
