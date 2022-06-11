using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAPI.Application.Dtos.Http
{
    public class BasicHttpResponseDTO<T>
    {
        public bool Success { get; set; }
        public ICollection<string> Errors { get; set; }
        public T Result { get; set; }
    }
}
