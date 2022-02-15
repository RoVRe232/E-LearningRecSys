using System.ComponentModel.DataAnnotations;

namespace RecSysApi.Domain.Entities
{
    public class PluginData
    {
        public Politube PolitubeData { get; set; }
        [StringLength(256)]
        public string OA { get; set; }
    }
}