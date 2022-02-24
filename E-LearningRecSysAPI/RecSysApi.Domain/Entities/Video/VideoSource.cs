using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Entities
{
    public class VideoSource
    {
        public Guid VideoSourceID { get; set; }
        [StringLength(256)]
        public string Type { get; set; }
        public ICollection<VideoBoxDescription> VideoBoxDescriptions { get; set; }
        [StringLength(256)]
        public string Poster { get; set; }
    }
}
