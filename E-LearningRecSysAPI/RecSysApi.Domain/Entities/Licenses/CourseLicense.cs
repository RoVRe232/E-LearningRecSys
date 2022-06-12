using RecSysApi.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Entities.Licenses
{
    public class CourseLicense
    {
        public Guid CourseLicenseID { get; set; }
        public Guid? AccountID { get; set; }
        public Guid? CourseID { get; set; }

    }
}
