using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exception
{
    public class CandidateCannotCreatedException : IOException
    {
        public CandidateCannotCreatedException() { }
        public CandidateCannotCreatedException(string message) : base(message) { }
    }
}
