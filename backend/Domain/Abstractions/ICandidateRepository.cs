using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstractions
{
    public interface ICandidateRepository
    {
        Task<IEnumerable<Candidate>> GetAsync();
        Task<Candidate?> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(Candidate candidate);
        Task<bool> UpdateAsync(Candidate candidate);
        Task RemoveAsync(Guid id);
    }
}
