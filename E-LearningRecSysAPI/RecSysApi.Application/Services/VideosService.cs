using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecSysApi.Application.Dtos.Courses;
using RecSysApi.Application.Dtos.Search;
using RecSysApi.Application.Dtos.Video;
using RecSysApi.Application.Interfaces;
using RecSysApi.Domain.Entities;
using RecSysApi.Domain.Interfaces;
using System;
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

        public async Task<VideoDTO> GetVideo(Guid videoId)
        {
            var videoMetadata = await _videoRepository.GetQuery(e => e.VideoID == videoId)
                .Include(e => e.Section)
                .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync();
            return _mapper.Map<VideoDTO>(videoMetadata);
        }

        public async Task<List<Video>> SearchForVideos(SearchQueryDTO query)
        {
            if (query.KeyPhrases != null && query.KeyPhrases.Count() > 0 && query.KeyPhrases.All(e => !String.IsNullOrEmpty(e)))
            {
                string searchWords = query.KeyPhrases.Aggregate("", (acc, e) => acc += e);
                var queryResults = await _videoRepository
                    .GetQuery(e => EF.Functions.FreeText(e.Description, searchWords) ||
                         EF.Functions.FreeText(e.Title, searchWords) ||
                         EF.Functions.FreeText(e.Keywords, searchWords))
                    .Include(e => e.Section.Course)
                    .Skip(query.PaginationOptions.Skip)
                    .Take(query.PaginationOptions.Take)
                    .ToListAsync();
                return queryResults;

            } else {
                var queryResults = await _videoRepository
                    .GetQuery(e => true)
                    .Include(e => e.Section.Course)
                    .OrderBy(e => e.VideoID)
                    .Skip(query.PaginationOptions.Skip)
                    .Take(query.PaginationOptions.Take)
                    .ToListAsync();
                return queryResults;
            }
        }

        public List<VideoDTO> FilterVideosBelongingToCourses(List<VideoDTO> videos, List<CourseDTO> courses)
        {
            return videos.Where(e => courses.Any(x => x.CourseID == e.Section.CourseID)).ToList();
        }

        public List<VideoDTO> MapVideosToVideoDTOs(List<Video> videos)
        {
            return _mapper.Map<List<VideoDTO>>(videos);
        }
    }
}
