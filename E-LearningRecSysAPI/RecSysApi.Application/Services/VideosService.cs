using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecSysApi.Application.Dtos.Search;
using RecSysApi.Application.Dtos.Video;
using RecSysApi.Application.Interfaces;
using RecSysApi.Domain.Entities;
using RecSysApi.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecSysApi.Application.Services
{
    public class VideosService : IVideosService
    {
        private IVideosStorageService _storageService;
        private IVideoRepository _videoRepository;
        private readonly IMapper _mapper;

        public VideosService(IVideosStorageService storageService, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _storageService = storageService;
            _videoRepository = unitOfWork.Videos;
            _mapper = mapper;
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

        public async Task<List<Video>> SearchForVideos(SearchQueryDTO query)
        {
            string searchWords = query.KeyPhrases.Aggregate("", (acc, e) => acc += e);
            var queryResults = await _videoRepository
                .GetQuery(e => EF.Functions.FreeText(e.Description, searchWords) &&
                     EF.Functions.FreeText(e.Title, searchWords))
                .Skip(query.PaginationOptions.Skip)
                .Take(query.PaginationOptions.Take)
                .ToListAsync();
            return queryResults;
        }

        public List<VideoDTO> MapVideosToVideoDTOs(List<Video> videos)
        {
            return _mapper.Map<List<VideoDTO>>(videos);
        }
    }
}
