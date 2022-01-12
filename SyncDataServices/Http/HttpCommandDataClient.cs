using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Http
{
    public class HttpCommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpCommandDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task SendPlatformToCommad(PlatformReadDto plat)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(plat),
                Encoding.UTF8,
                "application/json"
            );
            var reponse = await _httpClient.PostAsync($"{_configuration["CommandService"]}/api/c/Platforms", httpContent);
            if(reponse.IsSuccessStatusCode)
            {
                Console.WriteLine("--> Sync POST to CommandService was Ok!");
            }
            else
            {
                Console.WriteLine("--> Sync POST to CommandService was NOT Ok!");
            }
        }
    }
}