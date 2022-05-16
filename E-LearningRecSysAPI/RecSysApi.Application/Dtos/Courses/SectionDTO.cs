using RecSysApi.Application.Dtos.Video;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Dtos.Courses
{
    public class SectionDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ThumbnailImage { get; set; }
        public ICollection<VideoDTO> Videos { get; set; }
    }
}
