using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OrdersAPI.Application.Interfaces;
using RecSysApi.Domain.Commons.Models;
using RecSysApi.Domain.Interfaces;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrdersAPI.Infrastructure.Services
{
    //Consider switching HttpClient to IHttpClientFactory
    public sealed class HttpService : IHttpService
    {
        private readonly ILogger<HttpService> _logger;

        public HttpClient httpClient { get; }

        public HttpService(ILogger<HttpService> logger)
        {
            _logger = logger;
            httpClient = new HttpClient();
        }

        public async Task<HttpResponseMessage> SendPostRequestToApiAsync<T>(RequestUrl<T> requestUrl)
        {
            var signupFormModelJSON = JsonConvert.SerializeObject(requestUrl.Content);
            var requestContent = new StringContent(signupFormModelJSON, Encoding.UTF8, "application/json");
            var absoluteUri = $"{requestUrl.Protocol}://{requestUrl.Domain}/{requestUrl.Path}{(requestUrl.QueryParams != null ? requestUrl.QueryParams.Count > 0 ? requestUrl.QueryParams.Aggregate("?", (acc, member) => { acc += $"{member.Item1}={member.Item2}&"; return acc; }) : "" : "")}{(string.IsNullOrEmpty(requestUrl.Anchor) ? requestUrl.Anchor : "")}";
            var requestUri = new Uri(absoluteUri);
            _logger.LogInformation($"Sending POST request to: {requestUri}");
            var response = await httpClient.PostAsync(requestUri, requestContent);

            return response;
        }

        public async Task<HttpResponseMessage> SendGetRequestToApiAsync<T>(RequestUrl<T> requestUrl)
        {
            var absoluteUriPath = $"{requestUrl.Protocol}://{requestUrl.Domain}/{requestUrl.Path}{(requestUrl.QueryParams != null ? requestUrl.QueryParams.Count > 0 ? requestUrl.QueryParams.Aggregate("?", (acc, member) => { acc += $"{member.Item1}={member.Item2}&"; return acc; }) : "" : "")}{(string.IsNullOrEmpty(requestUrl.Anchor) ? "" : requestUrl.Anchor)}";
            var requestUri = new Uri(absoluteUriPath);
            _logger.LogInformation($"Sending GET request to: {requestUri}");
            var response = await httpClient.GetAsync(requestUri);
            return response;
        }
    }
}
