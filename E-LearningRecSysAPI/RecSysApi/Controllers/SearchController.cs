using Microsoft.AspNetCore.Mvc;
using RecSysApi.Application.Dtos.Search;
using RecSysApi.Application.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecSysApi.Presentation.Controllers
{
    [Route("api/courses")]
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
        public async Task<ActionResult<ICollection<SearchResultsDTO>>> Search(SearchQueryDTO searchQueryDTO)
        {
            var coursesResults = _coursesServices.MapCoursesToCourseDTOs(await _coursesServices.SearchForCourses(searchQueryDTO));
            var videosResults = _videosService.SearchVideo();
        }

    }
}
