using AutoMapper;
using Microsoft.Extensions.Logging;
using RecSysApi.Application.Dtos.Courses;
using RecSysApi.Application.Interfaces;
using RecSysApi.Domain.Entities.Products;
using RecSysApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var result =  await _courseRepository.AddAsync(course);
            await _unitOfWork.SaveChangesAsync();
            return result;
        }
    }
}
