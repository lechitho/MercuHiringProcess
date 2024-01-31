using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure
{
    public class RepositoryOptions
    {
        public string JsonFilePath { get; set; } = string.Empty;
    }
    public class CacheOptions
    {
        public int ExpireSecond { get; set; }
    }
}
