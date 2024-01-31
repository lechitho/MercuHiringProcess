using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enum
{
    public enum Stage
    {
        [EnumMember(Value = "Applied")]
        Applied = 0,
        [EnumMember(Value = "Interviewing")]
        Interviewing = 1,
        [EnumMember(Value = "Offered")]
        Offered = 2,
        [EnumMember(Value = "Hired")]
        Hired = 3,
    }
}
