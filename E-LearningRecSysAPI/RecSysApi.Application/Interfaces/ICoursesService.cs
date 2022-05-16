using RecSysApi.Application.Dtos.Courses;
using RecSysApi.Application.Dtos.Search;
using RecSysApi.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Interfaces
{
    public interface ICoursesService
    {
        public Task<Course> CreateCourse(CourseDTO course);
        public Task<List<Course>> SearchCourse(SearchQueryDTO query);
    }
}
