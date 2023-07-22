using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeedTunes.Dto.RequestDto.PlayListDTO;
using WeedTunes.Dto.RequestDto.SongDTOs;
using WeedTunes.Dto.ResponseDto.SongDTOs;
using WeedTunes.Dto.ResponseDto.UserPlayListDTO;
using WeedTunes.Services.Interface;
using WeedTunes.Utilities.Helpers;
using WeedTunes.Utilities.Pagination;

namespace WeedTunes.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PlayListController : BaseController
    {
        private readonly IPlayListService _playListService;

        public PlayListController(IPlayListService playListService)
        {
            _playListService = playListService;
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        public async Task<IActionResult> CreateSong([FromBody] CreatePlayListDTO model)
        {
            try
            {
                return ReturnResponse(await _playListService.CreatePlayList(model));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost("Songs-To-Play-List")]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        public async Task<IActionResult> AddSongsToPlayList([FromBody] AddSongsToPlayListDTO model) 
        {
            try
            {
                return ReturnResponse(await _playListService.AddSongsToPlayList(model));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }


        [HttpGet]
        [Route("Songs-By-Play-List")]
        [ProducesResponseType(typeof(BaseResponse<PagedList<UserPlayListDTO>>), 200)]
        public async Task<IActionResult> GetSongByPlayListTitle([FromQuery] PlayListQueryDTO model) 
        {
            try
            {
                return ReturnResponse(await _playListService.GetSongsByPlayList(model));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("Songs/No-Pagination/{playListId}")]
        [ProducesResponseType(typeof(BaseResponse<List<SongDTO>>), 200)]
        public async Task<IActionResult> GetSongByPlayListTitle(Guid playListId) 
        {
            try
            {
                return ReturnResponse(await _playListService.GetSongsByPlayList(playListId));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpDelete("")]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        public async Task<IActionResult> DeletePlayList([FromBody] DeletePlayListDTO model)
        {
            try
            {
                return ReturnResponse(await _playListService.DeleteSongFromPlayList(model));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}
