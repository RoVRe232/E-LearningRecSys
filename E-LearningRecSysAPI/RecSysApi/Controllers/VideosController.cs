using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecSysApi.Application.Dtos;
using RecSysApi.Application.Dtos.Video;
using RecSysApi.Application.Interfaces;
using RecSysApi.Application.Models;
using RecSysApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecSysApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        private readonly ILogger<VideosController> _logger;
        private readonly IVideosLookupService _videosLookupService;
        public VideosController(ILogger<VideosController> logger, IVideosLookupService videosLookupService)
        {
            _logger = logger;
            _videosLookupService = videosLookupService;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Video>>> GetVideosForQuery([FromQuery] string keywords, int take, int skip, string sessionId)
        {
            var getVideosQueryDto = new GetVideosQueryDto
            {
                Content = new QueryContentDto { BulkKeywords = keywords },
                Take = take,
                Skip = skip,
                SessionId = sessionId
            };
            var queryResponse = await _videosLookupService.LookupForVideosAsync(getVideosQueryDto);
            if (queryResponse == null || queryResponse.Count() == 0)
                return NotFound(queryResponse);

            return Ok(queryResponse);
        }

        //[HttpGet]
        //public ActionResult<ICollection<Video>> GetVideos([FromQuery] string keywords)
        //{
        //    var result = new List<Video>();
        //    var video = new Video
        //    {
        //        Id = System.Guid.NewGuid(),
        //        Title = "VideoTitle",
        //        Thumbnail = "thumbnailUrl"
        //    };
        //    result.Add(video);

        //    return Ok(result);
        //}
    }
}
