using RecSysApi.Application.Dtos.Courses;
using RecSysApi.Application.Dtos.Search;
using RecSysApi.Domain.Entities.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RecSysApi.Application.Interfaces
{
    public interface ICoursesService
    {
        public Task<Course> CreateCourse(CourseDTO course);
        public Task<List<Course>> SearchForCourses(SearchQueryDTO query);
        public List<CourseDTO> MapCoursesToCourseDTOs(List<Course> courses);
    }
}
