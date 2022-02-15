using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Interfaces
{
    public interface IVideosJsonService
    {
        public void ProcessVideosJson(string videosUpvJson);
    }
}
