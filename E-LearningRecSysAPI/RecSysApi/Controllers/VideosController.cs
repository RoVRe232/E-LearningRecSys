using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RecSysApi.Application.Dtos;
using RecSysApi.Application.Dtos.Http;
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
    [Route("api/videos")]
    public class VideosController : ApiBaseController
    {
        private readonly ILogger<VideosController> _logger;
        private readonly IVideosService _videosService;
        public VideosController(ILogger<VideosController> logger, IVideosService videosService)
        {
            _logger = logger;
            _videosService = videosService;
        }

        [HttpGet]
        public async Task<ActionResult<BasicHttpResponseDTO<VideoDTO>>> GetVideo([FromQuery] Guid videoId)
        {
            var result = await _videosService.GetVideo(videoId);
            if (result == null)
                return BadRequest();
            return Ok(new BasicHttpResponseDTO<VideoDTO>
            {
                Success = true,
                Errors = new List<string>(),
                Result = result
            });
        }

        [HttpPost]
        [Route("add-source-content")]
        [DisableRequestSizeLimit]
        public async Task<string> AddSourceContent([FromForm] VideoSourceUploadDTO source)
        {
            return await _videosService.AddVideoSourceContent(source);
        }

        [HttpGet]
        [Route("get-source-content")]
        public async Task<string> GetSourceContent([FromQuery] string id)
        {
            var result = await _videosService.GetVideoContent(id);
            //TODO Return video in better format
            return "data:video/mp4;base64," + System.Convert.ToBase64String(result);
        }
    }
}
