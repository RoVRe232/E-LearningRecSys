using RecSysApi.Application.Dtos.Courses;
using RecSysApi.Application.Dtos.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Dtos.Search
{
    public class SearchResultsDTO
    {
        public ICollection<CourseDTO> Courses { get; set; }
        public ICollection<VideoDTO> Videos { get; set; }
    }
}
