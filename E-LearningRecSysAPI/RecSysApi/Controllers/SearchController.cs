﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly ICoursesService _coursesServices;
        private readonly IVideosService _videosService;
        private readonly ISessionService _sessionService;
        public SearchController(ICoursesService coursesService, IVideosService videosService, ISessionService sessionService)
        {
            _coursesServices = coursesService;
            _videosService = videosService;
            _sessionService = sessionService;
        }

        [HttpPost]
        [Route("query")]
        public async Task<ActionResult<BasicHttpResponseDTO<SearchResultsDTO>>> Query(SearchQueryDTO searchQueryDTO)
        {
            if (searchQueryDTO.PaginationOptions.Take <= 0)
                searchQueryDTO.PaginationOptions.Take = 10;
            var coursesResults = await CheckIfOwnedForAuthenticatedAccounts(
                _coursesServices.MapCoursesToCourseDTOs(await _coursesServices.SearchForCourses(searchQueryDTO)));
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

        private async Task<List<CourseDTO>> CheckIfOwnedForAuthenticatedAccounts(List<CourseDTO> courses)
        {
            var claimsIdentiy = User.Claims.GetEnumerator();
            do
            {
                var claim = claimsIdentiy.Current;
                if (claim != null && claim.Type != null && claim.Type == ClaimTypes.NameIdentifier)
                {
                    var userDetails = await _sessionService.GetAuthenticatedUserAsync(new Guid(claim.Value));

                    return await _coursesServices.CheckIfCoursesAreOwnedByAccount(courses, userDetails.AccountID);
                }

            } while (claimsIdentiy.MoveNext());
            return courses;
        }

        private async Task<List<FilterDTO>> GetAvailableFilters([FromBody] List<FilterDTO> appliedFilters)
        {
            _coursesServices.GetAvailableFilters(appliedFilters);
        }

    }
}
