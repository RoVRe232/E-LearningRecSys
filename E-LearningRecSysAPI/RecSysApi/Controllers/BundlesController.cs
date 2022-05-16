using Microsoft.AspNetCore.Mvc;
using RecSysApi.Domain.Entities.Products;
using System;
using System.Threading.Tasks;

namespace RecSysApi.Presentation.Controllers
{
    [Route("api/bundles")]
    public class BundlesController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<Bundle>> GetBundle([FromQuery] Guid courseId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult<Bundle>> CreateBundle([FromBody] Bundle course)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public async Task<ActionResult<Bundle>> UpdateBundle([FromBody] Bundle course)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<ActionResult<Bundle>> DeleteBundle([FromBody] Bundle course)
        {
            throw new NotImplementedException();
        }
    }
}
