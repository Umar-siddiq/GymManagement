using GymManagement.Data.Models;
using System.Text;
using System.Text.Json;

namespace GymManagement.Utility.Services
{
    public class GymUserApiService
    {
        private readonly HttpClient _httpClient;

		public GymUserApiService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}


		public async Task<HttpResponseMessage> CreateGymUserAsync(GymUser gymUser)
		{
			var json = JsonSerializer.Serialize(gymUser);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			return await _httpClient.PostAsync("https://localhost:7055/api/Admin/GymUserApi", content);
		}
		public async Task<HttpResponseMessage> UpdateGymUserAsync(string gymUserId, GymUser gymUser) 
		{
			var json = JsonSerializer.Serialize(gymUser);
			var content = new StringContent(json, Encoding.UTF8 , "application/json");

			return await _httpClient.PutAsync($"https://localhost:7055/api/Admin/GymUserApi/{gymUserId}", content);
		}
		public async Task<HttpResponseMessage> SearchGymUserAsync(string query)
		{
			return await _httpClient.GetAsync($"https://localhost:7055/api/Admin/GymUserApi/search?query={query}");
		}

		public async Task<HttpResponseMessage> DeleteGymUserAsync(string gymUserId)
		{
			return await _httpClient.DeleteAsync($"https://localhost:7055/api/Admin/GymUserApi/{gymUserId}");
		}


	}
}