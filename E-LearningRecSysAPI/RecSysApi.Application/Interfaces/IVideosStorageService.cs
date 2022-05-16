using RecSysApi.Application.Dtos.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Interfaces
{
    public interface IVideosStorageService
    {
        public Task<string> StoreVideoToPermanentStorage(VideoSourceUploadDTO source);
        public Task<byte[]> GetVideoContentFromPermanentStorage(string id);
    }
}
