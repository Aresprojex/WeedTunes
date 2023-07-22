using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using WeedTunes.Dto.RequestDto.UserPlayListDTO;
using WeedTunes.Services.Interface;
using WeedTunes.Utilities.Helpers;
using WeedTunes.Dto.RequestDto.SongDTOs;
using WeedTunes.Dto.RequestDto.PlayListDTO;
using WeedTunes.Dto.ResponseDto.UserPlayListDTO;
using WeedTunes.Services.Implementation;
using WeedTunes.Utilities.Pagination;
using WeedTunes.Dto.ResponseDto.SongDTOs;
using System.Collections.Generic;

namespace WeedTunes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SongController : BaseController
    {
        private readonly ISongService _songService;

        public SongController(ISongService songService)
        {
            _songService = songService ?? throw new ArgumentNullException(nameof(songService));
        }


        [HttpPost("")]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        public async Task<IActionResult> CreateSong([FromBody] CreateSongDTO model)
        {
            try
            {
                return ReturnResponse(await _songService.CreateSong(model));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut("Strain")]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        public async Task<IActionResult> AddStrainToASong([FromBody] AddStrainToSongDTO model) 
        {
            try
            {
                return ReturnResponse(await _songService.AddStrainToSong(model));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(BaseResponse<PagedList<SongDTO>>), 200)]
        public async Task<IActionResult> GetAllSongs([FromQuery] SongQueryDTO model)
        {
            try
            {
                return ReturnResponse(await _songService.GetAllSongs(model));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("No-Pagination")]
        [ProducesResponseType(typeof(BaseResponse<List<SongDTO>>), 200)]
        public async Task<IActionResult> GetSongs()
        {
            try
            {
                return ReturnResponse(await _songService.GetAllSongs());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}
