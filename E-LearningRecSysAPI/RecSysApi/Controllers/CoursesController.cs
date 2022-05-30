using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecSysApi.Application.Dtos.Courses;
using RecSysApi.Application.Dtos.Http;
using RecSysApi.Application.Interfaces;
using RecSysApi.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RecSysApi.Presentation.Controllers
{
    [Route("api/courses")]
    [Authorize(Policy = "AuthOnly")]
    public class CoursesController : ApiBaseController
    {
        private readonly ICoursesService _coursesServices;
        private readonly ISessionService _sessionService;
        public CoursesController(ICoursesService coursesService, ISessionService sessionService)
        {
            _coursesServices = coursesService;
            _sessionService = sessionService;
        }

        [HttpGet]
        public async Task<ActionResult<Course>> GetCourse([FromQuery] Guid courseId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<BasicHttpResponseDTO<bool>>> CreateCourse([FromBody] CourseDTO course)
        {

            //TODO ADD CLAIMS EXTRACTOR MIDDLEWARE
            var claimsIdentiy = User.Claims.GetEnumerator();
            do
            {
                var claim = claimsIdentiy.Current;
                if (claim != null && claim.Type != null && claim.Type == ClaimTypes.NameIdentifier)
                {
                    var userDetails = await _sessionService.GetAuthenticatedUserAsync(new Guid(claim.Value));
                    course.AccountID = userDetails.AccountID;

                    var courseResult = await _coursesServices.CreateCourse(course);

                    //TODO RETURN DTO HERE!!
                    var response = new BasicHttpResponseDTO<bool>
                    {
                        Success = true,
                        Errors = new List<string>(),
                        Result = true
                    };
                    return Ok(response);
                }

            } while (claimsIdentiy.MoveNext());

            return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult<Course>> UpdateCourse([FromBody] CourseDTO course)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public async Task<ActionResult<Course>> DeleteCourse([FromBody] Guid courseId)
        {
            throw new NotImplementedException();
        }
    }
}
