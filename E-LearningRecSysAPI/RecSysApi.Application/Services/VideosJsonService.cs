using RecSysApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Services
{
    public class VideosJsonService : IVideosJsonService
    {
        public void ProcessVideosJson(string videosUpvJson)
        {
            //TODO Deserialize videosUpvJson into DTOs and pass them down to domain to be handled
            //TODO when reaching domain, they will be processed(checked if they are in the database already, and if not, passed to infrastructure)
            throw new NotImplementedException();
        }
    }
}
