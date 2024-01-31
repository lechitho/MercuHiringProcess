using HiringProcessService.API.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Application.Test
{
    public class CandidateControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _client;
        public CandidateControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = factory.CreateClient();
        }
        [Fact, TestPriority(1)]
        public async Task CreateCandidate_WithValidCandidate_ReturnsCreatedAtActionAndId()
        {
            // Arrange
            var newCandidate = new CandidateModel { Name = "David", Stage = "Applied", Phone = "0987654321", Email = "janesmith@example.com" };

            // Act
            var response = await _client.PostAsJsonAsync("api/Candidates", newCandidate);

            // Assert
            response.EnsureSuccessStatusCode();
            var id = await response.Content.ReadFromJsonAsync<Guid>();
            Assert.NotEqual(Guid.Empty, id);
        }
        [Fact, TestPriority(2)]
        public async Task GetCandidates_ReturnsOkAndListOfCandidates()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync("api/Candidates");

            // Assert
            response.EnsureSuccessStatusCode();
            var actualCandidates = await response.Content.ReadFromJsonAsync<IEnumerable<CandidateModel>>();

        }


        [Fact, TestPriority(3)]
        public async Task UpdateCandidate_WithValidIdAndCandidate_ReturnsNoContent()
        {
            // Arrange
            var existingCandidateId = Guid.NewGuid();
            var updatedCandidate = new CandidateModel { Id = existingCandidateId,  Name = "David", Stage = "Applied", Phone = "0987654321", Email = "janesmith@example.com" };

            // Act
            var response = await _client.PutAsJsonAsync($"api/Candidates/{existingCandidateId}", updatedCandidate);

            // Assert
            response.EnsureSuccessStatusCode();
        }


        [Fact, TestPriority(4)]
        public async Task DeleteCandidate_WithValidId_ReturnsNoContent()
        {
            // Arrange
            var existingCandidateId = Guid.NewGuid();

            // Act
            var response = await _client.DeleteAsync($"api/Candidates/{existingCandidateId}");

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
