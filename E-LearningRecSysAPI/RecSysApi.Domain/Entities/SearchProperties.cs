using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecSysApi.Domain.Entities
{
    public class SearchProperties
    {
        [StringLength(256)]
        public string Title { get; set; }
        public ICollection<string> Owner { get; set; }
        public string Transcription { get; set; }
    }
}