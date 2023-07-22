using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeedTunes.Dto;
using WeedTunes.Dto.RequestDto.UserFavouriteSongDTO;
using WeedTunes.Dto.RequestDto.UserPlayListDTO;
using WeedTunes.Dto.ResponseDto.UserFavouriteSongDTO;
using WeedTunes.Dto.ResponseDto.UserPlayListDTO;
using WeedTunes.Utilities.Helpers;
using WeedTunes.Utilities.Pagination;

namespace WeedTunes.Services.Interface
{
    public interface IUserPlayListService
    {
        Task<BaseResponse<bool>> CreatePlayList(CreateUserPlayListDTO model); 
        Task<BaseResponse<bool>> AddSongToUserPlayList(AddSongToUserPlayListDTO model);   
        Task<BaseResponse<bool>> AddSongsToUserPlayList(AddSongsToUserPlayListDTO model, Guid userId);   
        Task<BaseResponse<bool>> RemoveSongFromUserPlayList(RemoveSongFromPlayListDTO model, Guid userId);   
        Task<BaseResponse<PagedList<UserPlayListDTO>>> GetAUserPlayLists(UserPlayListQueryDTO model, Guid userId);    
        Task<BaseResponse<PagedList<UserPlayListSongsDTO>>> GetAUserPlayListSongs(UserPlayListSongQueryDTO model);
        Task<BaseResponse<bool>> AddFavouriteSongsToUser(AddFavouriteSongsToUserDTO model); 
        Task<BaseResponse<bool>> RemoveFavouriteSongsFromUser(RemoveFavouriteSongsFromUserDTO model, Guid userId);
        Task<BaseResponse<PagedList<UserFavouriteSongDTO>>> GetAUserFavouriteSongs(UserFavouriteSongQueryDTO model, Guid userId);

    }
}
