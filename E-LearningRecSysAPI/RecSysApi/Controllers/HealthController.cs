using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecSysApi.Presentation.Controllers
{
    public class HealthController : ApiBaseController
    {
        [HttpGet]
        public string Index()
        {
            return "Healthy";
        }
    }
}
