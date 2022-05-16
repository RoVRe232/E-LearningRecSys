using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Dtos.Video
{
    public class VideoDTO
    {
        public Guid VideoID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public VideoSourceDTO Source { get; set; }
        public string Keywords { get; set; }
        public string Thumbnail { get; set; }
        //public ICollection<VideoSlides> Slides { get; set; }
        public bool Hidden { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DeletionDate { get; set; }
        public string Language { get; set; }
        public bool HiddenInSearches { get; set; }
        public double Duration { get; set; }
        public string Transcription { get; set; }
        public string Author { get; set; }
    }
}
