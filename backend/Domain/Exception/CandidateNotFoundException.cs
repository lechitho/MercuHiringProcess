using Domain.Exception.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exception
{
    public class CandidateNotFoundException: NotFoundException
    {
        public CandidateNotFoundException(Guid candidateId) : base($"Candidate with id {candidateId} was not found") { }
    }
}
