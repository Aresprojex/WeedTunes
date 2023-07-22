using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using WeedTunes.Dto;
using WeedTunes.Services;
using WeedTunes.Services.Interface;
using WeedTunes.Dto.RequestDto.UserPlayListDTO;
using Microsoft.AspNetCore.Authorization;
using WeedTunes.Utilities.Helpers;
using WeedTunes.Utilities.Pagination;
using WeedTunes.Utilities;
using WeedTunes.Dto.ResponseDto.UserPlayListDTO;
using WeedTunes.Dto.RequestDto.UserFavouriteSongDTO;

namespace WeedTunes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserPlayListController : BaseController
    {
        private readonly IUserPlayListService _userPlayListService;

        public UserPlayListController(IUserPlayListService userPlayListService)
        {
            _userPlayListService = userPlayListService ?? throw new ArgumentNullException(nameof(userPlayListService));
        } 
  
        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        public async Task<IActionResult> CreatePlayList([FromBody] CreateUserPlayListDTO model)  
        {
            try
            {
                return ReturnResponse(await _userPlayListService.CreatePlayList(model));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpPost("Song-To-Play-List")]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        public async Task<IActionResult> AddSongToUserPlayList([FromBody] AddSongToUserPlayListDTO model) 
        {
            try
            {
                return ReturnResponse(await _userPlayListService.AddSongToUserPlayList(model));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }


        [HttpPost("Songs-To-Play-List/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        public async Task<IActionResult> AddSongsToUserPlayList([FromBody] AddSongsToUserPlayListDTO model, Guid userId)
        {
            try
            {
                return ReturnResponse(await _userPlayListService.AddSongsToUserPlayList(model, userId));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }      

        [HttpGet]
        [Route("{userId}")]
        [ProducesResponseType(typeof(BaseResponse<PagedList<UserPlayListDTO>>), 200)]
        public async Task<IActionResult> GetAUserPlayLists([FromQuery] UserPlayListQueryDTO model, Guid userId) 
        {
            try
            {
                return ReturnResponse(await _userPlayListService.GetAUserPlayLists(model, userId));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpGet]
        [Route("Play-List-Songs")]
        [ProducesResponseType(typeof(BaseResponse<PagedList<UserPlayListDTO>>), 200)]
        public async Task<IActionResult> GetAUserPlayListSongs([FromQuery] UserPlayListSongQueryDTO model)
        {
            try
            {
                return ReturnResponse(await _userPlayListService.GetAUserPlayListSongs(model));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }


        [HttpPost("Favourite-Song")]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        public async Task<IActionResult> AddFavouriteSongsToUser([FromBody] AddFavouriteSongsToUserDTO model)
        {
            try
            {
                return ReturnResponse(await _userPlayListService.AddFavouriteSongsToUser(model));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        } 

        [HttpGet]
        [Route("Favourite-Song/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<PagedList<UserPlayListDTO>>), 200)]
        public async Task<IActionResult> GetAUserFavouriteSongs([FromQuery] UserFavouriteSongQueryDTO model, Guid userId) 
        {
            try
            {
                return ReturnResponse(await _userPlayListService.GetAUserFavouriteSongs(model, userId));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpDelete("Song-From-Play-List/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        public async Task<IActionResult> RemoveSongFromUserPlayList([FromBody] RemoveSongFromPlayListDTO model, Guid userId)
        {
            try
            {
                return ReturnResponse(await _userPlayListService.RemoveSongFromUserPlayList(model, userId));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }

        [HttpDelete("Favourite-Song/{userId}")]
        [ProducesResponseType(typeof(BaseResponse<bool>), 200)]
        public async Task<IActionResult> RemoveFavouriteSongFromUser([FromBody] RemoveFavouriteSongsFromUserDTO model, Guid userId) 
        {
            try
            {
                return ReturnResponse(await _userPlayListService.RemoveFavouriteSongsFromUser(model, userId));
            }
            catch (Exception ex)
            {
                return HandleError(ex);
            }
        }
    }
}
