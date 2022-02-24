using System;
using System.Collections.Generic;

namespace RecSysApi.Domain.Entities
{
    public class VideoMetadata
    {
        public Guid VideoMetadataID { get; set; } 
        public string Keywords { get; set; }
    }
}