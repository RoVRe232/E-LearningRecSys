using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecSysApi.Application.Dtos.Courses;
using RecSysApi.Application.Dtos.Http;
using RecSysApi.Application.Dtos.Orders;
using RecSysApi.Application.Interfaces;
using RecSysApi.Domain.Commons.Models;
using RecSysApi.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
        private readonly IHttpService _httpService;
        public CoursesController(ICoursesService coursesService, ISessionService sessionService, IHttpService httpService)
        {
            _coursesServices = coursesService;
            _sessionService = sessionService;
            _httpService = httpService;
        }

        [Route("owned-courses")]
        [HttpGet]
        public async Task<ActionResult<BasicHttpResponseDTO<List<CourseDTO>>>> GetOwnedCourses()
        {
            var userDetails = await _sessionService.GetAuthenticatedUserFromClaimsAsync(User.Claims.GetEnumerator());
            var courseResult = await _coursesServices.GetOwnedCourses(userDetails.AccountID);
            if (courseResult == null)
                return BadRequest();
            return Ok(new BasicHttpResponseDTO<List<CourseDTO>>
            {
                Success = true,
                Errors = new List<string>(),
                Result = courseResult
            });
        }

        [Route("course")]
        [HttpGet]
        public async Task<ActionResult<BasicHttpResponseDTO<CourseDTO>>> GetCourse([FromQuery] Guid courseId)
        {
            var userDetails = await _sessionService.GetAuthenticatedUserFromClaimsAsync(User.Claims.GetEnumerator());
            
            var courseResult = await _coursesServices.GetCourse(courseId, userDetails != null ? userDetails.AccountID : new Guid());
            if(courseResult == null)
                return BadRequest();
            return Ok(new BasicHttpResponseDTO<CourseDTO>
            {
                Success = true,
                Errors = new List<string>(),
                Result = courseResult
            });
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

        [Route("new-order")]
        [HttpPost]
        public async Task<ActionResult<BasicHttpResponseDTO<bool>>> PurchaseCourses([FromBody] OrderDTO order)
        {
            //TODO ADD CLAIMS EXTRACTOR MIDDLEWARE
            var claimsIdentiy = User.Claims.GetEnumerator();
            do
            {
                var claim = claimsIdentiy.Current;
                if (claim != null && claim.Type != null && claim.Type == ClaimTypes.NameIdentifier)
                {
                    var userDetails = await _sessionService.GetAuthenticatedUserAsync(new Guid(claim.Value));
                    order.AccountID = userDetails.AccountID;

                    HttpResponseMessage requestResult;
                    if (Environment.GetEnvironmentVariable("DOCKER_DATABASE") != null && Environment.GetEnvironmentVariable("DOCKER_DATABASE") == "true")
                        requestResult = await _httpService.SendPostRequestToApiAsync(new RequestUrl<OrderDTO>
                        {
                            RequestUrlID = Guid.NewGuid(),
                            Content = order,
                            Protocol = "http",
                            Domain = "host.docker.internal:6002",
                            Path = "api/orders/create-order"
                        });
                    else
                    {
                        requestResult = await _httpService.SendPostRequestToApiAsync(new RequestUrl<OrderDTO>
                        {
                            RequestUrlID = Guid.NewGuid(),
                            Content = order,
                            Protocol = "https",
                            Domain = "localhost:6002",
                            Path = "api/orders/create-order"
                        });
                    }

                    if(requestResult.IsSuccessStatusCode)
                        return Ok(new BasicHttpResponseDTO<bool>
                        {
                            Success = true,
                            Errors = new List<string>(),
                            Result = true
                        });
                    return BadRequest(new BasicHttpResponseDTO<bool>
                    {
                        Success = false,
                        Errors = new List<string>() { "Something went wrong" },
                        Result = false
                    });
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
