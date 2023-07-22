using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WeedTunes.Dto;
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
    public interface ISongService
    {
        Task<BaseResponse<bool>> CreateSong(CreateSongDTO model); 
        Task<BaseResponse<bool>> AddStrainToSong(AddStrainToSongDTO model);
        Task<BaseResponse<List<SongDTO>>> GetAllSongs();
        Task<BaseResponse<PagedList<SongDTO>>> GetAllSongs(SongQueryDTO model);

    }
}
