using RecSysApi.Domain.Commons.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAPI.Application.Interfaces
{
    public interface IHttpService
    {
        public Task<HttpResponseMessage> SendPostRequestToApiAsync<T>(RequestUrl<T> requestUrl);
        public Task<HttpResponseMessage> SendGetRequestToApiAsync<T>(RequestUrl<T> requestUrl);
    }
}
