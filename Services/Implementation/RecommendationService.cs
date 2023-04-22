using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        public async Task<BaseResponse<IEnumerable<RecommendationDto>>> Recommend(GetRecommendationDto getRecommendation)
        {
            var httpClient = new HttpClient();
            
            var authModel = new SpotifyAuthDto
            {
                client_id = "myclientId",
                grant_type = "client_credentials"
            };

            var response = await httpClient
                .Post("https://accounts.spotify.com/api/token", authModel, "application/x-www-form-urlencoded");

            var authData = await response.ReadContentAs<SpotifyAuthResponseDto>();

            return new BaseResponse<IEnumerable<RecommendationDto>>();
        }
    }
}
