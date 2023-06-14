using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace CRM_Api_Senvang.Helpers
{
    public class OpenAIRequest
    {
        private readonly string _baseUrl = "https://api.openai.com/v1/chat/completions";
        private readonly string _apiKey;

        public OpenAIRequest(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<string> SendRequest(string model, string message)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders
         .Accept
         .Add(new MediaTypeWithQualityHeaderValue("application/json"));//ACCEPT header;
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var request = new
            {
                model = model,
                messages = new[] { new { role = "user", content = message } },
                max_tokens = 250
            };

            var jsonRequest = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            var httpResponse = await httpClient.PostAsync(_baseUrl, httpContent);

            if (httpResponse.IsSuccessStatusCode)
            {
                var jsonResult = await httpResponse.Content.ReadAsStringAsync();
                return jsonResult;
            }
            else
            {
                throw new Exception("Request failed with status code: " + httpResponse.StatusCode);
            }
        }
    }
}

