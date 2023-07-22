using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using WeedTunes.Dto.RequestDto.SongDTOs;
using WeedTunes.Services.Interface;
using WeedTunes.Utilities.Helpers;
using WeedTunes.Dto.RequestDto.GenreDTO;
using WeedTunes.Services.Implementation;
using System.Collections.Generic;
using WeedTunes.Dto.ResponseDto.SongDTOs;
using WeedTunes.Dto.ResponseDto.GenreDTO;

namespace WeedTunes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : BaseController
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
        }


        [HttpPost("")]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        public async Task<IActionResult> CreateGenre([FromBody] CreateGenreDTO model) 
        {
            try
            {
                return ReturnResponse(await _genreService.CreateGenre(model));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPut("Song")]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        public async Task<IActionResult> AddGenreToASong([FromBody] AddGenreToSongDTO model)
        {
            try
            {
                return ReturnResponse(await _genreService.AddGenreToSong(model));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("No-Pagination")]
        [ProducesResponseType(typeof(BaseResponse<List<GenreDTO>>), 200)]
        public async Task<IActionResult> GetAllGenres()
        {
            try
            {
                return ReturnResponse(await _genreService.GetAllGenres());
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}
