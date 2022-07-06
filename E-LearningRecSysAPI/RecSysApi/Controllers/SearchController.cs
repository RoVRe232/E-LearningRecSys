using Microsoft.AspNetCore.Mvc;
using RecSysApi.Application.Dtos.Courses;
using RecSysApi.Application.Dtos.Http;
using RecSysApi.Application.Dtos.Search;
using RecSysApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecSysApi.Presentation.Controllers
{
    [Route("api/search")]
    public class SearchController : ApiBaseController
    {
        private readonly ICoursesService _coursesService;
        private readonly IVideosService _videosService;
        private readonly ISessionService _sessionService;
        public SearchController(ICoursesService coursesService, IVideosService videosService, ISessionService sessionService)
        {
            _coursesService = coursesService;
            _videosService = videosService;
            _sessionService = sessionService;
        }

        [HttpPost]
        [Route("query")]
        public async Task<ActionResult<BasicHttpResponseDTO<SearchResultsDTO>>> Query(SearchQueryDTO searchQueryDTO)
        {
            if (searchQueryDTO.PaginationOptions.Take <= 0)
                searchQueryDTO.PaginationOptions.Take = 50;
            var databaseCourses = await _coursesService.SearchForCourses(searchQueryDTO);

            var courseFilters = searchQueryDTO.Filters;
            if (courseFilters == null || courseFilters.Count == 0)
                courseFilters = _coursesService.GetAvailableFilters(databaseCourses);

            var coursesResults = await CheckIfOwnedForAuthenticatedAccounts(
                _coursesService.MapCoursesToCourseDTOs(_coursesService.FilterCourses(databaseCourses, searchQueryDTO.Filters)));

            var videosResults = _videosService.FilterVideosBelongingToCourses(
                _videosService.MapVideosToVideoDTOs(await _videosService.SearchForVideos(searchQueryDTO)), coursesResults);
            return Ok(new BasicHttpResponseDTO<SearchResultsDTO>
            {
                Success = true,
                Errors = new List<string>(),
                Result = new SearchResultsDTO
                {
                    Courses = coursesResults,
                    Videos = videosResults,
                    Filters = courseFilters
                }
            });
        }

        private async Task<List<CourseDTO>> CheckIfOwnedForAuthenticatedAccounts(List<CourseDTO> courses)
        {
            var claimsIdentiy = User.Claims.GetEnumerator();
            do
            {
                var claim = claimsIdentiy.Current;
                if (claim != null && claim.Type != null && claim.Type == ClaimTypes.NameIdentifier)
                {
                    var userDetails = await _sessionService.GetAuthenticatedUserAsync(new Guid(claim.Value));

                    return await _coursesService.CheckIfCoursesAreOwnedByAccount(courses, userDetails.AccountID);
                }

            } while (claimsIdentiy.MoveNext());
            return courses;
        }
    }
}
