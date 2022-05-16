using RecSysApi.Application.Dtos.Courses;
using RecSysApi.Application.Dtos.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Interfaces
{
    public interface IVideosService
    {
        public Task<string> StoreVideoMetadata(VideoDTO video, CourseDTO videosUpvJson);
        public Task<string> AddVideoSourceContent(VideoSourceUploadDTO videoSource);
        public Task<byte[]> GetVideoContent(string id);
    }
}
