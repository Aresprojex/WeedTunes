using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeedTunes.Dto.RequestDto.PlayListDTO;
using WeedTunes.Dto.RequestDto.SongDTOs;
using WeedTunes.Dto.RequestDto.UserPlayListDTO;
using WeedTunes.Dto.ResponseDto.SongDTOs;
using WeedTunes.Dto.ResponseDto.UserPlayListDTO;
using WeedTunes.Utilities;
using WeedTunes.Utilities.Helpers;
using WeedTunes.Utilities.Pagination;

namespace WeedTunes.Services.Interface
{
    public interface IPlayListService
    {
        Task<BaseResponse<bool>> CreatePlayList(CreatePlayListDTO model);
        Task<BaseResponse<bool>> AddSongsToPlayList(AddSongsToPlayListDTO model); 
        Task<BaseResponse<bool>> DeleteSongFromPlayList(DeletePlayListDTO model);
        Task<BaseResponse<PagedList<SongDTO>>> GetSongsByPlayList(PlayListQueryDTO model);        
        Task<BaseResponse<List<SongDTO>>> GetSongsByPlayList(Guid playListId);   

    }
}
