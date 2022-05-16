using RecSysApi.Application.Dtos.Courses;
using RecSysApi.Application.Dtos.Video;
using RecSysApi.Application.Interfaces;
using RecSysApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Services
{
    public class VideosService : IVideosService
    {
        private IVideosStorageService _storageService;
        public VideosService(IVideosStorageService storageService)
        {
            _storageService = storageService;
        }
        public async Task<string> StoreVideoMetadata(VideoDTO video, CourseDTO course)
        {
            throw new NotImplementedException();  
            //string base64data = video.Source.VideoContent.Replace("data:video/mp4;base64,", "");
            //byte[] result = Convert.FromBase64String(base64data);
            //return await _storageService.StoreVideoToPermanentStorage(result, video.Source.Location);
        }

        public async Task<string> AddVideoSourceContent(VideoSourceUploadDTO videoSource)
        {

            return await _storageService.StoreVideoToPermanentStorage(videoSource);
        }

        public async Task<byte[]> GetVideoContent(string id)
        {
            var videoContent = await _storageService.GetVideoContentFromPermanentStorage(id);
            return videoContent;
            //return Encoding.ASCII.GetString(videoContent);
        }
    }
}
