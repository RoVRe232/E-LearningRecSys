using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Interfaces
{
    public interface IHashGeneratorService
    {
        public Task<string> GenerateHashAsync(string from);
        public string GenerateHash(string from);
    }
}
