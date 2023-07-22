using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeedTunes.Dto;
using WeedTunes.Services;
using WeedTunes.Utilities;
using WeedTunes.Utilities.Helpers;
using WeedTunes.Utilities.Pagination;

namespace WeedTunes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StrainController : BaseController
    {
        private readonly IStrainService _strainService;
        public StrainController(IStrainService strainService)
        {
            _strainService = strainService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<StrainDto>), 200)]
        [ProducesResponseType(typeof(BaseResponse<StrainDto>), 500)]
        public async Task<IActionResult> CreateStrain([FromBody]CreateStrianDto strianDto)
        {
            return ReturnResponse(await _strainService.CreateStrain(strianDto));
        }

        [HttpGet]
        [Route("{strianId}")]
        [ProducesResponseType(typeof(BaseResponse<StrainDto>), 200)]
        [ProducesResponseType(typeof(BaseResponse<>), 404)]
        public async Task<IActionResult> GetStrain(Guid strianId)
        {
            return ReturnResponse(await _strainService.GetStrain(strianId));
        }

        [HttpGet]
        [Route("strains")]
        [ProducesResponseType(typeof(BaseResponse<PagedList<StrainDto>>), 200)]
        public async Task<IActionResult> GetStrains([FromBody] BaseSearchViewModel search)
        {
            return ReturnResponse(await _strainService.GetStrains(search));
        }

    }
}
