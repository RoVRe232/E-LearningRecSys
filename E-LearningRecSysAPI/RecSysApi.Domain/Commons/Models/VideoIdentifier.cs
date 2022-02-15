using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Commons.Models
{
    public class VideoIdentifier
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public ICollection<string> Tags { get; set; }
    }
}
