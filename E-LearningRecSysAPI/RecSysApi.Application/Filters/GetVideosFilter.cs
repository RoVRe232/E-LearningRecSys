using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Filters
{
    public class GetVideosFilter : PaginationFilter
    {
        public string Name { get; set; }
        public List<string> Tags { get; set; }
    }
}
