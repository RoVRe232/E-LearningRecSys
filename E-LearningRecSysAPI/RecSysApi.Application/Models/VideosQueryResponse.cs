using RecSysApi.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Models
{
    public class VideosQueryResponse
    {
        public ICollection<Video> Videos { get; set; }
    }
}
