using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecSysApi.Domain.Entities
{
    public class SearchProperties
    {
        public Guid SearchPropertiesID { get; set; }
        [StringLength(256)]
        public string Title { get; set; }
        public string Transcription { get; set; }
    }
}