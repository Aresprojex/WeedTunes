using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeedTunes.Dto;
using WeedTunes.Services;
using WeedTunes.Utilities.Helpers;

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

    }
}
