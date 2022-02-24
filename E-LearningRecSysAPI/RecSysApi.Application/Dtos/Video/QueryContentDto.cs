using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Dtos.Video
{
    public class QueryContentDto
    {
        public string BulkKeywords { get; set; }
        public List<string> SplitKeywords { get; set; } = new List<string>();
    }
}
