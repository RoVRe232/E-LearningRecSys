using System.Collections.Generic;

namespace RecSysApi.Domain.Entities
{
    public class VideoMetadata
    {
        public ICollection<string> Keywords { get; set; }
    }
}