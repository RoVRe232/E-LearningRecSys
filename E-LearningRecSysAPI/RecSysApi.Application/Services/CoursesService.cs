using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecSysApi.Application.Dtos.Courses;
using RecSysApi.Application.Dtos.Search;
using RecSysApi.Application.Interfaces;
using RecSysApi.Domain.Entities.Products;
using RecSysApi.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RecSysApi.Application.Services
{
    public class CoursesService : ICoursesService
    {
        private readonly ILogger<CoursesService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;
        private readonly IVideosService _videosService;

        public CoursesService(ILogger<CoursesService> logger, IUnitOfWork unitOfWork, IMapper mapper, IVideosService videosService)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _courseRepository = unitOfWork.Courses;
            _videosService = videosService;
        }

        public async Task<Course> CreateCourse(CourseDTO courseDto)
        {
            var course = _mapper.Map<Course>(courseDto);
            var result = await _courseRepository.AddAsync(course);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }

        public async Task<List<Course>> SearchForCourses(SearchQueryDTO query)
        {
            string searchWords = query.KeyPhrases.Aggregate("", (acc, e) => acc += e);
            var queryResults = await _courseRepository
                .GetQuery(e => EF.Functions.FreeText(e.LargeDescription, searchWords) ||
                     EF.Functions.FreeText(e.SmallDescription, searchWords) ||
                     EF.Functions.FreeText(e.Name, searchWords))
                .ToListAsync();
            return queryResults;
        }

        public List<CourseDTO> MapCoursesToCourseDTOs(List<Course> courses)
        {
            return _mapper.Map<List<CourseDTO>>(courses);
        }
    }
}
