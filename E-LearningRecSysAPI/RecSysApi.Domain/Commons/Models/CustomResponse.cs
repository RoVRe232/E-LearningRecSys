﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain.Entities.Common
{
    public class CustomResponse<T>
    {
        public Guid Id { get; set; }
        public HttpStatusCode Status { get; set; }
        public string Message { get; set; }
        public T Content { get; set; }
    }
}
