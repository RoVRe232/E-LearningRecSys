using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Entities
{
    public class RequestUrl<T>
    {
        public Guid RequestUrlID { get; set; }
        public T Content { get; set; }
        public string Protocol { get; set; }
        public string Domain { get; set; }
        public string Path { get; set; }
        public ICollection<Tuple<string,string>> QueryParams { get; set; }
        public string Anchor { get; set; }
    }
}
