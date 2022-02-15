using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RecSysApi.Domain.Commons.Models;
using RecSysApi.Domain.Entities;
using RecSysApi.Domain.Interfaces;
using RecSysApi.Infrastructure.Commons.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Infrastructure.Services
{
    public class VideosRetrievalService : IVideosRetrievalService
    {
        private readonly ILogger<VideosRetrievalService> _logger;
        private readonly IHttpService _httpService;
        private readonly IVideoRepository _videoRepository;
        public VideosRetrievalService(ILogger<VideosRetrievalService> logger, IHttpService httpService, IVideoRepository videoRepository)
        {
            _logger = logger;
            _httpService = httpService;
            _videoRepository = videoRepository;
        }

        public async Task<ICollection<Video>> GetInternalVideosForQueryAsync(ICollection<VideoIdentifier> videosIdentifiers)
        {
            var videos = _videoRepository.GetQuery(e => videosIdentifiers.Any(r => r.Title == e.Title)).AsEnumerable();

            if(videos != null && videos.ToList().Count() > 0)
                return videos.ToList();
            else
            {
                var videoIdentifier = videosIdentifiers.FirstOrDefault();
                // Return hardcoded data for test
                var testVideo = new Video
                {
                    Thumbnail = "hardcoded thumbnail",
                    Title = videoIdentifier.Title,
                    Author = videoIdentifier.Author,
                    Tags = videoIdentifier.Tags
                };
                var result = new List<Video>();
                result.Add(testVideo);
                
                return result;
            }

        }

        public async Task<ICollection<VideoIdentifier>> GetVideosIdentifiersForQueryAsync(Query query)
        {
            var requestUrl = new RequestUrl<string>
            {
                Id = Guid.NewGuid(),
                Content = $"{{\"keyword\":\"{query.BulkKeywords}\"}}",
                Protocol = "http",
                Domain = Constants.kSearchForAQueryDomain,
                Path = "videos/",
            };
            var response = await _httpService.SendPostRequestToApiAsync(requestUrl);
            if (!response.IsSuccessStatusCode)
                response = await _httpService.SendPostRequestToApiAsync(requestUrl);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = ((Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(content)).ToObject<List<VideoIdentifier>>();

                return result;
            } 
            else
            {
                //Return hardcoded data for test
                var videoIdentifier = new VideoIdentifier { Id = "az123456", Author = "Author1", Tags= null, Title="Video1" };
                var result = new List<VideoIdentifier>();
                result.Add(videoIdentifier);
                return result;
            }
        }
    }
}
