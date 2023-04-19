using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeedTunes.Dto;
using WeedTunes.Services;
using WeedTunes.Utilities.Helpers;

namespace WeedTunes.Services
{
    public class RecommendationService : IRecommendationService
    {
        public async Task<BaseResponse<IEnumerable<RecommendationDto>>> Recommend(GetRecommendationDto getRecommendation)
        {
            return new BaseResponse<IEnumerable<RecommendationDto>>();
        }
    }
}
