using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Dtos.Video
{
    public class GetVideosQueryDto
    {
        public QueryContentDto Content { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public string SessionId { get; set; }
    }
}
