using RecSysApi.Application.Dtos.Courses;
using RecSysApi.Application.Dtos.Search;
using RecSysApi.Application.Dtos.Video;
using RecSysApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecSysApi.Application.Interfaces
{
    public interface IVideosService
    {
        public Task<string> AddVideoSourceContent(VideoSourceUploadDTO videoSource);
        public Task<byte[]> GetVideoContent(string id);
        public Task<List<Video>> SearchForVideos(SearchQueryDTO query);
        public List<VideoDTO> MapVideosToVideoDTOs(List<Video> courses);
        public Task<VideoDTO> GetVideo(Guid videoId);
        public List<VideoDTO> FilterVideosBelongingToCourses(List<VideoDTO> videos, List<CourseDTO> courses);
    }
}
