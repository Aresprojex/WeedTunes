using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeedTunes.Dto;
using WeedTunes.Services;

namespace WeedTunes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecommendationController : BaseController
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService ?? throw new ArgumentNullException(nameof(recommendationService));
        }

        /// <summary>
        /// Recommend spotify musics to users 
        /// </summary>
        /// <param name="recommendationDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Get([FromBody] GetRecommendationDto recommendationDto)
        {
            try
            {
                return ReturnResponse(await _recommendationService.Recommend(recommendationDto));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}
