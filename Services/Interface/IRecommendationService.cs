using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeedTunes.Dto;
using WeedTunes.Utilities.Helpers;

namespace WeedTunes.Services
{
    public interface IRecommendationService
    {
        Task<BaseResponse<SpotifyAuthResponseDto>> GetToken();
        Task<BaseResponse<IEnumerable<RecommendationDto>>> GetRecommendations(GetRecommendationDto getRecommendation);
    }
}
