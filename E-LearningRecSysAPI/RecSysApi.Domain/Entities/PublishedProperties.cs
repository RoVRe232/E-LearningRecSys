using System.ComponentModel.DataAnnotations;

namespace RecSysApi.Domain.Entities
{
    public class PublishedProperties
    {
        [StringLength(256)]
        public string Status { get; set; }
    }
}