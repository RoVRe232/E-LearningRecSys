using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecSysApi.Application.Dtos.Courses;
using RecSysApi.Application.Dtos.Search;
using RecSysApi.Application.Interfaces;
using RecSysApi.Domain.Entities.Products;
using RecSysApi.Domain.Interfaces;
using System;
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
            if (query.KeyPhrases != null && query.KeyPhrases.Count() > 0 && query.KeyPhrases.All(e => !String.IsNullOrEmpty(e)))
            {
                string searchWords = query.KeyPhrases.Aggregate("", (acc, e) => acc += e);
                var queryResults = await _courseRepository
                    .GetCoursesWithAccountByExpressionAsync(e => EF.Functions.FreeText(e.LargeDescription, searchWords) ||
                         EF.Functions.FreeText(e.SmallDescription, searchWords) ||
                         EF.Functions.FreeText(e.Name, searchWords))
                    .Skip(query.PaginationOptions.Skip)
                    .Take(query.PaginationOptions.Take)
                    .ToListAsync();
                return queryResults;
            }
            else
            {
                var queryResults = await _courseRepository
                    .GetCoursesWithAccountByExpressionAsync(e => true)
                    .OrderBy(e => e.CourseID)
                    .Skip(query.PaginationOptions.Skip)
                    .Take(query.PaginationOptions.Take)
                    .ToListAsync();
                return queryResults;
            }
        }

        public async Task<CourseDTO> GetCourse(Guid courseId)
        {
            var queryResult = await _courseRepository.GetQuery(e => e.CourseID == courseId)
                .Include(e => e.Price)
                .Include(e => e.Account)
                .Include(e => e.Sections)
                .ThenInclude(e => e.Videos)
                .FirstOrDefaultAsync();
            return _mapper.Map<CourseDTO>(queryResult);
        }

        public async Task<List<CourseDTO>> GetOwnedCourses(Guid accountId)
        {
            var ownedLicensesIds = (await _unitOfWork.CourseLicenses.GetQuery(e => e.AccountID == accountId).ToArrayAsync())
                .Select(e=> e.CourseID);

            var ownedCourses = await _unitOfWork.Courses.GetQuery(e => ownedLicensesIds.Contains(e.CourseID)).ToListAsync();
            return _mapper.Map<List<CourseDTO>>(ownedCourses);
        }

        public List<CourseDTO> MapCoursesToCourseDTOs(List<Course> courses)
        {
            return _mapper.Map<List<CourseDTO>>(courses);
        }

        public async Task<List<CourseDTO>> CheckIfCoursesAreOwnedByAccount(List<CourseDTO> courses, Guid AccountID)
        {
            var checkedCourses = new List<CourseDTO>();
            foreach (var result in courses)
            {
                var owned = await _unitOfWork.CourseLicenses.FirstOrDefaultAsync(e => e.CourseID == result.CourseID && e.AccountID == AccountID);
                result.Owned = false;
                if (owned != null)
                    result.Owned = true;
                checkedCourses.Add(result);
            }
            return checkedCourses;
        }
    }
}
