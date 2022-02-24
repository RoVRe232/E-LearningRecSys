using Microsoft.Extensions.Logging;
using RecSysApi.Application.Dtos.Video;
using RecSysApi.Application.Interfaces;
using RecSysApi.Application.Models;
using RecSysApi.Domain.Commons.Models;
using RecSysApi.Domain.Entities;
using RecSysApi.Domain.Entities.Common;
using RecSysApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Services
{
    public class VideosLookupService : IVideosLookupService
    {
        private ILogger<VideosLookupService> _logger;
        private IVideosRetrievalService _videosRetrievalService;
        public VideosLookupService(ILogger<VideosLookupService> logger,IVideosRetrievalService videosRetrievalService)
        {
            _videosRetrievalService = videosRetrievalService;
            _logger = logger;
        }

        public async Task<ICollection<Video>> LookupForVideosAsync(GetVideosQueryDto getVideosQueryDto)
        {
            Query getVideosQuery = new Query
            {
                Id = Guid.NewGuid(),
                BulkKeywords = getVideosQueryDto.Content.BulkKeywords,
                SplitKeywords = getVideosQueryDto.Content.SplitKeywords
            };
            var videosIdentifiers = await _videosRetrievalService.GetVideosIdentifiersForQueryAsync(getVideosQuery);
            if (videosIdentifiers == null || videosIdentifiers.Count() == 0)
                return null;

            var internalVideosResponse = await _videosRetrievalService.GetInternalVideosForQueryAsync(videosIdentifiers);
            if (internalVideosResponse == null || internalVideosResponse.Count()==0)
                return null;

            return internalVideosResponse;
        }
    }
}
