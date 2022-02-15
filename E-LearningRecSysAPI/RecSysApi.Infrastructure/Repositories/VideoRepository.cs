using Microsoft.Extensions.Logging;
using RecSysApi.Domain.Entities;
using RecSysApi.Domain.Interfaces;
using RecSysApi.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Infrastructure.Repositories
{
    public class VideoRepository : Repository<Video>, IVideoRepository
    {
        private readonly ILogger<VideoRepository> _logger;
        public VideoRepository(RecSysApiContext recSysApiContext ,ILogger<VideoRepository> logger) : base(recSysApiContext)
        {
            _logger = logger;
        }
        public VideoRepository(RecSysApiContext dbContext) : base(dbContext) { }
        public List<Video> GetVideosWithIds(List<Guid> VideosIds)
        {
            return GetQuery(video => VideosIds.Contains(video.Id)).ToList();
        }
    }
}
