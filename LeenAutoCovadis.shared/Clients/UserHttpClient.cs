using LeenAutoCovadis.shared.Dtos;
using System.Text.Json;

namespace LeenAutoCovadis.shared.Clients;

public class UserHttpClient
{
    private readonly HttpClient client;
    private readonly JsonSerializerOptions jsonOptions;

    public UserHttpClient(IHttpClientFactory httpClientFactory)
    {
        client = httpClientFactory.CreateClient(nameof(UserHttpClient));
        client.BaseAddress = new Uri("https://localhost:7166/User");

        jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<UserDto[]> GetUsers()
    {
        var response = await client.GetAsync(string.Empty);

        if(!response.IsSuccessStatusCode)
        {
            return [];
        }

        var content = await response.Content.ReadAsStringAsync();
        var users = JsonSerializer.Deserialize<UserDto[]>(content, jsonOptions);

        if(users is null)
        {
            return [];
        }

        return users;
    }
}
