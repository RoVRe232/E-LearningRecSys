using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Entities.Products
{
    public class Course
    {
        public Guid CourseID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid PriceID { get; set; }
        
        public virtual Price Price { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
