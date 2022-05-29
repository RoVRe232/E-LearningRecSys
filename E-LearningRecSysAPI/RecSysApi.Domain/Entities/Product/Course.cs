using System;
using System.Collections.Generic;

namespace RecSysApi.Domain.Entities.Products
{
    public class Course
    {
        public Guid CourseID { get; set; }
        public Guid? AccountID { get; set; }
        public string Name { get; set; }
        public string SmallDescription { get; set; }
        public string LargeDescription { get; set; }
        public string ThumbnailImage { get; set; }
        public string Keywords { get; set; }
        public double Hours { get; set; }
        public Guid PriceID { get; set; }
        public virtual Price Price { get; set; }
        public virtual Account.Account Account { get; set; }
        public virtual ICollection<Section> Sections { get; set; }
    }
}
