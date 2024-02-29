using System.Net.Http.Headers;
using System.Text.Json;
using TestApi.Models;

namespace TestApi.Services
{
    public class CatApiService: ICatApiService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _clientFactory;
        public CatApiService(IConfiguration configuration, IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            _configuration = configuration;
        }

        public async Task<List<CatImage>> GetCatImages(int numberToGet)
        {
            var baseUrl = _configuration["CatApiSettings:BaseUrl"];
            var httpClient = _clientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(baseUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            List<CatImage> catImages = new List<CatImage>();
            HttpResponseMessage response = await httpClient.GetAsync($"images/search?limit={numberToGet}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                catImages = JsonSerializer.Deserialize<List<CatImage>>(data);
            }

            return catImages;
        }
    }
}
