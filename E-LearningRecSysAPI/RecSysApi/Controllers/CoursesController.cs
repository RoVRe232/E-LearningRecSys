using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecSysApi.Application.Dtos.Courses;
using RecSysApi.Application.Interfaces;
using RecSysApi.Domain.Entities.Products;
using System;
using System.Threading.Tasks;

namespace RecSysApi.Presentation.Controllers
{
    [Route("api/courses")]
    [Authorize(Policy = "AuthOnly")]
    public class CoursesController : ApiBaseController
    {
        private readonly ICoursesService _coursesServices;
        public CoursesController(ICoursesService coursesService)
        {
            _coursesServices = coursesService;
        }

        [HttpGet]
        public async Task<ActionResult<Course>> GetCourse([FromQuery] Guid courseId)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<ActionResult<bool>> CreateCourse([FromBody] CourseDTO course)
        {
            var courseResult = await _coursesServices.CreateCourse(course);
            if (courseResult == null)
                return BadRequest();
            return Ok(true);
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
