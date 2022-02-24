using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Entities.Products
{
    public class Section
    {
        public Guid SectionID { get; set; }
        public Guid CourseID { get; set; }

        public Course Course { get; set; }
        public ICollection<Video> Videos { get; set; }
    }
}
