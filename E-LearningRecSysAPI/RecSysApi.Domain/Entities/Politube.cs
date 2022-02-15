using System.ComponentModel.DataAnnotations;

namespace RecSysApi.Domain.Entities
{
    public class Politube
    {
        [StringLength(256)]
        public string Id { get; set; }
        [StringLength(256)]
        public int Indexer { get; set; }
    }
}