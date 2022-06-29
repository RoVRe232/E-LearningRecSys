﻿using AutoMapper;
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

        public async Task<CourseDTO> GetCourse(Guid courseId, Guid accountId = new Guid())
        {
            var queryResult = await _courseRepository.GetQuery(e => e.CourseID == courseId)
                .Include(e => e.Price)
                .Include(e => e.Account)
                .Include(e => e.Sections)
                .ThenInclude(e => e.Videos)
                .Include(e => e.Sections)
                .ThenInclude(e => e.Course)
                .FirstOrDefaultAsync();
            var mappedQueryResult = _mapper.Map<CourseDTO>(queryResult);

            if (accountId != Guid.Empty)
                mappedQueryResult.Owned = await _unitOfWork.CourseLicenses.GetQuery(e => e.AccountID == accountId && courseId == e.CourseID).AnyAsync();
            return mappedQueryResult;
        }

        public async Task<List<CourseDTO>> GetOwnedCourses(Guid accountId)
        {
            var ownedLicensesIds = (await _unitOfWork.CourseLicenses.GetQuery(e => e.AccountID == accountId).ToArrayAsync())
                .Select(e => e.CourseID);

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

        public List<Course> FilterCourses(List<Course> courses, List<FilterDTO> filters)
        {
            if (filters != null)
            {
                var filteredCourses = courses.Where(e => filters.All(x => CheckFilter(e, x))).ToList();
                return filteredCourses;
            }
            return courses;
        }

        public List<FilterDTO> GetAvailableFilters(List<Course> courses)
        {
            var results = GetBaseFilters();
            var minPrice = new PriceDTO { Amount = 9999999.9 };
            var maxPrice = new PriceDTO { Amount = 0 };
            var minDuration = 9999999.9;
            var maxDuration = 0.0;
            foreach (var course in courses)
            {
                try
                {
                    if (results.ContainsKey("MainKeyword"))
                    {
                        var mainKeyword = course.Keywords.Split(" ").Length > 0 ? course.Keywords.Split(" ")[0] : course.Keywords;
                        if (!results["MainKeyword"].Any(e => e.Value == mainKeyword))
                            results["MainKeyword"]?.Add(new FilterDTO
                            {
                                Name = mainKeyword,
                                Value = mainKeyword,
                                Type = Dtos.Enums.FilterType.CHECKBOX,
                            });
                    }
                }
                catch (Exception ex) { }

                if (results.ContainsKey("AuthorName") && !results["AuthorName"].Any(e => e.Value == course.Account.AccountID.ToString()))
                    results["AuthorName"]?.Add(new FilterDTO
                    {
                        Name = course.Account.Name,
                        Value = course.Account.AccountID.ToString(),
                        Type = Dtos.Enums.FilterType.CHECKBOX,
                    });

                if (course.Price.Amount <= minPrice.Amount)
                    minPrice = _mapper.Map<PriceDTO>(course.Price);
                if (course.Price.Amount >= maxPrice.Amount)
                    maxPrice = _mapper.Map<PriceDTO>(course.Price);

                if (course.Hours <= minDuration)
                    minDuration = course.Hours;
                if (course.Hours >= maxDuration)
                    maxDuration = course.Hours;
            }
            results["Price"]?.Add(new FilterDTO
            {
                Name = "Price",
                Value = minPrice.Currency,
                LowerBound = minPrice.Amount,
                UpperBound = maxPrice.Amount,
                LowValue = minPrice.Amount,
                HighValue = maxPrice.Amount,
                Type = Dtos.Enums.FilterType.INTERVAL
            });

            results["Duration"]?.Add(new FilterDTO
            {
                Name = "Duration",
                Value = "Hours",
                LowerBound = minDuration,
                UpperBound = maxDuration,
                LowValue = minDuration,
                HighValue = maxDuration,
                Type = Dtos.Enums.FilterType.INTERVAL
            });

            var finalResults = new List<FilterDTO>();

            finalResults.Add(new FilterDTO
            {
                Type = Dtos.Enums.FilterType.CHECKBOX,
                Name = "MainKeyword",
                Value = "MainKeyword",
                SubFilters = results["MainKeyword"]
            });
            finalResults.Add(new FilterDTO
            {
                Type = Dtos.Enums.FilterType.CHECKBOX,
                Name = "AuthorName",
                Value = "AuthorName",
                SubFilters = results["AuthorName"]
            });
            finalResults.Add(new FilterDTO
            {
                Type = Dtos.Enums.FilterType.INTERVAL,
                Name = "Price",
                Value = "Price",
                SubFilters = results["Price"]
            });
            finalResults.Add(new FilterDTO
            {
                Type = Dtos.Enums.FilterType.INTERVAL,
                Name = "Duration",
                Value = "Duration",
                SubFilters = results["Duration"]
            });

            return finalResults;
        }

        public Dictionary<string, List<FilterDTO>> GetBaseFilters()
        {
            var result = new Dictionary<string, List<FilterDTO>>
            {
                {"MainKeyword", new List<FilterDTO>() },
                {"AuthorName", new List<FilterDTO>() },
                {"Price", new List<FilterDTO>() },
                {"Duration", new List<FilterDTO>() },
            };
            return result;
        }

        private bool CheckFilter(Course course, FilterDTO filter)
        {
            if (filter.Type == Dtos.Enums.FilterType.CHECKBOX || filter.Type == Dtos.Enums.FilterType.INTERVAL)
            {
                switch (filter.Name)
                {
                    case "AuthorName":
                        if (filter.SubFilters.All(e => e.Checked == false))
                            return true;
                        if (filter.SubFilters.Any(e => e.Checked == true && course.Account.Name == e.Name))
                            return true;
                        break;
                    case "MainKeyword":
                        if (filter.SubFilters.All(e => e.Checked == false))
                            return true;
                        var mainKeyword = course.Keywords.Split(" ").Length > 0 ? course.Keywords.Split(" ")[0] : course.Keywords;
                        if (filter.SubFilters.Any(e => e.Checked == true && e.Value == mainKeyword))
                            return true;
                        break;
                    case "Price":
                        var priceFilter = filter.SubFilters.First();
                        if (priceFilter.LowValue > course.Price.Amount || priceFilter.HighValue < course.Price.Amount)
                            return false;
                        return true;
                    case "Duration":
                        var durationFilter = filter.SubFilters.First();
                        if (durationFilter.LowValue > course.Hours || durationFilter.HighValue < course.Hours)
                            return false;
                        return true;
                }
            }
            return false;
        }
    }
}
