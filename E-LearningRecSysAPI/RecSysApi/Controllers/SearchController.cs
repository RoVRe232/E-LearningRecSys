using Microsoft.AspNetCore.Mvc;
using RecSysApi.Application.Dtos.Search;
using RecSysApi.Application.Interfaces;
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
        public async Task<ActionResult<SearchResultsDTO>> Query(SearchQueryDTO searchQueryDTO)
        {
            var coursesResults = _coursesServices.MapCoursesToCourseDTOs(await _coursesServices.SearchForCourses(searchQueryDTO));
            var videosResults = _videosService.MapVideosToVideoDTOs(await _videosService.SearchForVideos(searchQueryDTO));
            return Ok(new SearchResultsDTO
            {
                Courses = coursesResults,
                Videos = videosResults
            });
        }

    }
}
