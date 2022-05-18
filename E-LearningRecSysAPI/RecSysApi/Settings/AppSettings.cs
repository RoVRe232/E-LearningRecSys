using RecSysApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecSysApi.Presentation.Settings
{
    public class AppSettings : IAppSettings
    {
        public string Secret { get; set; }
        public string DbConnectionString { get; set; }
    }
}
