using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Entities.Common
{
    public class Query
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        public string BulkKeywords { get; set; }
        public List<string> SplitKeywords { get; set; }
        public ICollection<Video> ConcreteResults { get; set; }
    }
}
