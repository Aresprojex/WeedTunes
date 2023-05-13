using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WeedTunes.Dto;
using WeedTunes.Services;
using WeedTunes.Utilities;
using WeedTunes.Utilities.Helpers;

namespace WeedTunes.Services
{
    public class RecommendationService : IRecommendationService
    {
        public async Task<BaseResponse<SpotifyAuthResponseDto>> GetToken()
        {
            var result = new BaseResponse<SpotifyAuthResponseDto>();

            var httpClient = new HttpClient();
            
            var authModel = new SpotifyAuthDto
            {
                client_id = "c452930ca7e54703b812e5d03727f71c",
                client_secret = "5d4ef4c3dc014b82991050032f3252ca",
                grant_type = "client_credentials"
            };


            var dataAsString = $"grant_type=client_credentials&client_id={authModel.client_id}&client_secret={authModel.client_secret}";

            var content = new StringContent(dataAsString, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            var response =  await httpClient.PostAsync("https://accounts.spotify.com/api/token", content);

            var authData = await response.ReadContentAs<SpotifyAuthResponseDto>();
            result.Data = authData;

            return result;
        }

        public Task<BaseResponse<IEnumerable<RecommendationDto>>> GetRecommendations(GetRecommendationDto getRecommendation)
        {
            throw new NotImplementedException();
        }
    }
}
