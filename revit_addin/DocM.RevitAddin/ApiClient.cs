// In ApiClient.cs
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocM.RevitAddin
{
    public class ApiClient
    {
        private static readonly HttpClient client = new HttpClient();

        public async Task<string> PostDataAsync(string jsonData)
        {
            // The URL of your local LLM service
            var apiUrl = "http://192.168.0.8:8000/api/llm";

            try
            {
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                response.EnsureSuccessStatusCode(); // Throws an exception if the HTTP response status is an error code.

                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;
            }
            catch (HttpRequestException e)
            {
                Logger.Log($"API Request Error: {e.Message}");
                return null;
            }
        }
    }
}