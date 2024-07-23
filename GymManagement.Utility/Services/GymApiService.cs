using GymManagement.Data.Models;
using System.Text;
using System.Text.Json;

namespace GymManagement.Utility.Services
{
    public class GymApiService
    {
        private readonly HttpClient _httpClient;

		public GymApiService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}


		public async Task<HttpResponseMessage> CreateGymAsync(Gym gym)
		{
			var json = JsonSerializer.Serialize(gym);
			var content = new StringContent(json, Encoding.UTF8, "application/json");

			return await _httpClient.PostAsync("https://localhost:7055/api/Admin/GymApi", content);
		}

		public async Task<HttpResponseMessage> DeleteGymAsync(int id)
		{
			return await _httpClient.DeleteAsync($"https://localhost:7055/api/Admin/GymApi/{id}");
		}
	}
}