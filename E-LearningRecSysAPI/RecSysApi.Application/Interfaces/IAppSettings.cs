using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Interfaces
{
    public interface IAppSettings
    {
        string Secret { get; set; }
        string DbConnectionString { get; set; }
    }
}
