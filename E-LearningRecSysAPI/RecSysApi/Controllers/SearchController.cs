using Microsoft.AspNetCore.Mvc;
using RecSysApi.Application.Dtos.Http;
using RecSysApi.Application.Dtos.Search;
using RecSysApi.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecSysApi.Presentation.Controllers
{
    [Route("api/search")]
    public class SearchController : ApiBaseController
    {
        private readonly ICoursesService _coursesServices;
        private readonly IVideosService _videosService;
        public SearchController(ICoursesService coursesService, IVideosService videosService)
        {
            _coursesServices = coursesService;
            _videosService = videosService;
        }

        [HttpPost]
        [Route("query")]
        public async Task<ActionResult<BasicHttpResponseDTO<SearchResultsDTO>>> Query(SearchQueryDTO searchQueryDTO)
        {
            //See https://www.bricelam.net/2020/08/08/mssql-freetext-and-efcore.html
            var coursesResults = _coursesServices.MapCoursesToCourseDTOs(await _coursesServices.SearchForCourses(searchQueryDTO));
            var videosResults = _videosService.MapVideosToVideoDTOs(await _videosService.SearchForVideos(searchQueryDTO));
            return Ok(new BasicHttpResponseDTO<SearchResultsDTO>
            {
                Success = true,
                Errors = new List<string>(),
                Result = new SearchResultsDTO
                {
                    Courses = coursesResults,
                    Videos = videosResults
                }
            });
        }

    }
}
