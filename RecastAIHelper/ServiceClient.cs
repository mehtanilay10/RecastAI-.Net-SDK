using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RecastAIHelper.Models;

namespace RecastAIHelper
{
    public class ServiceClient: IDisposable
    {
        private readonly HttpClient _client;

        public ServiceClient(string token, string userSlug)
        {
            var baseUrl = $"https://api.recast.ai/v2/users/{userSlug}/";
            _client = HttpClientFactory.Create(baseUrl, token);
        }

        protected async Task<string> Get(string path)
        {
            var response = await _client.GetAsync(path);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return responseContent;
            else if (response.StatusCode != System.Net.HttpStatusCode.BadRequest)
            {
                var exception = JsonConvert.DeserializeObject<ServiceResponse>(responseContent);
                throw new Exception($"{exception.Message}");
            }
            return null;
        }

        protected async Task<string> Post(string path)
        {
            var response = await _client.PostAsync(path, null);
            return await GetResponseContent(response);
        }

        protected async Task<string> Post<TRequest>(string path, TRequest requestBody)
        {
            using (var content = new ByteArrayContent(GetByteData(requestBody)))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await _client.PostAsync(path, content);
                return await GetResponseContent(response);
            }
        }

        protected async Task<string> Put<TRequest>(string path, TRequest requestBody)
        {
            using (var content = new ByteArrayContent(GetByteData(requestBody)))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var response = await _client.PutAsync(path, content);
                return await GetResponseContent(response);
            }
        }

        protected async Task<string> Delete(string path)
        {
            var response = await _client.DeleteAsync(path);
            return await GetResponseContent(response);
        }

        private async Task<string> GetResponseContent(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                var exception = JsonConvert.DeserializeObject<ServiceResponse>(responseContent);
                throw new Exception(exception.Message);
            }
            return responseContent;
        }

        private byte[] GetByteData<TRequest>(TRequest requestBody)
        {
            var settings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
            var body = JsonConvert.SerializeObject(requestBody, settings);
            return Encoding.UTF8.GetBytes(body);
        }

        public void Dispose() =>
            _client.Dispose();
    }
}
