using System.ComponentModel.DataAnnotations;

namespace RecSysApi.Domain.Entities
{
    public class VideoBoxDescription
    {
        [StringLength(256)]
        public string Mimetype { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        [StringLength(256)]
        public string Src { get; set; }
    }
}