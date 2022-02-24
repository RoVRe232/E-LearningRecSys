using RecSysApi.Domain.Entities.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Entities
{
    public class Video
    {
        [Key]
        [Required]
        public Guid VideoID { get; set; }
        public Guid SectionID { get; set; }
        public Section Section { get; set; }
        [Required]
        [StringLength(256)]
        public string Title { get; set; }
        public VideoSource Source { get; set; }
        public VideoMetadata Metadata { get; set; }
        [StringLength(256)]
        public string Thumbnail { get; set; }
        public ICollection<VideoSlides> Slides { get; set; }
        public bool Hidden { get; set; }
        public bool CanRead { get; set; }
        public bool CanWrite { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime DeletionDate { get; set; }
        [StringLength(256)]
        public string Repository { get; set; }
        [StringLength(256)]
        public string Language { get; set; }
        public bool HiddenInSearches { get; set; }
        public SearchProperties Search { get; set; }
        public bool HideSocial { get; set; }
        [StringLength(256)]
        public string Catalog { get; set; }
        public bool Published{ get; set; }
        public bool Unprocessed { get; set; }
        public bool ProcessSlides { get; set; }
        public double Duration { get; set; }
        public string Transcription { get; set; }
        [StringLength(256)]
        public string Author { get; set; }
    }
}
