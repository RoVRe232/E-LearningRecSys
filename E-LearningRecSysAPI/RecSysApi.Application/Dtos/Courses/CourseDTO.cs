using RecSysApi.Application.Dtos.Account;
using System;
using System.Collections.Generic;

namespace RecSysApi.Application.Dtos.Courses
{
    public class CourseDTO
    {
        public Guid CourseID { get; set; }
        public string Name { get; set; }
        public string SmallDescription { get; set; }
        public string LargeDescription { get; set; }
        public string ThumbnailImage { get; set; }
        public double Hours { get; set; }
        public PublisherDTO Author { get; set; }
        public PriceDTO Price { get; set; }
        public ICollection<SectionDTO> Sections { get; set; }
    }
}
