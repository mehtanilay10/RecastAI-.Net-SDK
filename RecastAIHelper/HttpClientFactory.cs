using System;
using System.Net.Http;

namespace RecastAIHelper
{
    public static class HttpClientFactory
    {
        public static HttpClient Create(string baseUrl, string token)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("Authorization", $"Token {token}");
            client.DefaultRequestHeaders.ConnectionClose = false;
            System.Net.ServicePointManager.FindServicePoint(client.BaseAddress).ConnectionLeaseTimeout = 60 * 1000; //1 minute
            return client;
        }
    }
}