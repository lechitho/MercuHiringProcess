using Application.Abstractions.Caching;
using Domain.Abstractions;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infastructure.Persistent
{
    public class JsonCandidateRepository : ICandidateRepository
    {
        private List<Candidate> _candidateData;
        private ICacheService _cacheService;

        private readonly string _jsonFilePath;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public JsonCandidateRepository(RepositoryOptions options, ICacheService cacheService, CacheOptions caching)
        {
            _cacheService = cacheService;
            _jsonFilePath = options.JsonFilePath;
            _jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };

            if (!File.Exists(_jsonFilePath))
            {
                File.WriteAllText(_jsonFilePath, "[]");
            }
            ReadCandidatesFromFile();
        }
        public async Task<IEnumerable<Candidate>> GetAsync() => _candidateData;

        public async Task<Candidate?> GetByIdAsync(Guid id) => _candidateData.Where(e => e.Id == id).SingleOrDefault();

        public async Task<Guid> AddAsync(Candidate candidate)
        {
            _candidateData.Add(candidate);

            await WriteCandidatesToFile();
            return candidate.Id;
        }

        public async Task<bool> UpdateAsync(Candidate candidate)
        {
            var existingCandidateIndex = _candidateData.FindIndex(e => e.Id == candidate.Id);

            if (existingCandidateIndex != -1)
            {
                _candidateData[existingCandidateIndex] = candidate;
                await WriteCandidatesToFile();
                return true;
            }
            return false;
        }

        public async Task RemoveAsync(Guid id)
        {
            var candidateToRemove = _candidateData.FirstOrDefault(e => e.Id == id);

            if (candidateToRemove != null)
            {
                _candidateData.Remove(candidateToRemove);
                await WriteCandidatesToFile();
            }
        }
        private void ReadCandidatesFromFile()
        {
            var cacheKey = $"jsonCandidates";

            var readDataFromFileFunc = async () =>
            {
                var candidates = new List<Candidate>();

                var json = await File.ReadAllTextAsync(_jsonFilePath);

                if (!string.IsNullOrWhiteSpace(json))
                {
                    candidates = JsonSerializer.Deserialize<IEnumerable<JsonCandidate>>(json, _jsonSerializerOptions)?.Select(e => Candidate.Create(e.Id, e.Name, e.Stage, e.Phone, e.Email)).ToList();
                }

                return candidates;
            };

            _candidateData = _cacheService.GetOrCreateAsync(cacheKey, readDataFromFileFunc).Result;

        }

        private async Task WriteCandidatesToFile()
        {
            using var fileStream = File.Create(_jsonFilePath);
            await JsonSerializer.SerializeAsync(fileStream, _candidateData, _jsonSerializerOptions);
        }
    }
}
