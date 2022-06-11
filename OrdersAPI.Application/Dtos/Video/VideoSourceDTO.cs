﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Dtos.Video
{
    public class VideoSourceDTO
    {
        public Guid VideoSourceID { get; set; }
        public string InternalID { get; set; }
        public string Type { get; set; }
        public string Location { get; set; }
    }
}
