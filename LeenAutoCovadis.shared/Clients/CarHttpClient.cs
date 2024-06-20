using LeenAutoCovadis.shared.Dtos;
using System.Text.Json;
using System.Threading.Tasks;

namespace LeenAutoCovadis.shared.Clients
{
    public class CarHttpClient
    {
        private readonly HttpClient client;
        private readonly JsonSerializerOptions jsonOptions;

        public CarHttpClient(IHttpClientFactory httpClientFactory)
        {
            client = httpClientFactory.CreateClient(nameof(CarHttpClient));
            client.BaseAddress = new Uri("https://localhost:7166/api/car");

            jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<CarDto?> GetCar()
        {
            var response = await client.GetAsync(string.Empty);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var cars = JsonSerializer.Deserialize<IEnumerable<CarDto>>(content, jsonOptions);

            return cars?.FirstOrDefault();
        }
    }
}
