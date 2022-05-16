using System;
using System.ComponentModel.DataAnnotations;

namespace RecSysApi.Domain.Entities
{
    public class VideoSlides
    {
        public Guid VideoSlidesID { get; set; }
        [StringLength(256)]
        public string Mimetype { get; set; }
        [StringLength(256)]
        public string Url { get; set; }
        [StringLength(256)]

        public string Thumb { get; set; }
        [StringLength(256)]
        public string Time { get; set; }
    }
}