using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public virtual PriceDTO Price { get; set; }
        public virtual ICollection<SectionDTO> Sections { get; set; }
    }
}
