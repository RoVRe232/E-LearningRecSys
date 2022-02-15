using RecSysApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Interfaces
{
    public interface IVideoRepository : IRepository<Video>
    {
        List<Video> GetVideosWithIds(List<Guid> VideosIds);
    }
}
