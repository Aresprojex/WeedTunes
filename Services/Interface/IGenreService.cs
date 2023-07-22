using System.Collections.Generic;
using System.Threading.Tasks;
using WeedTunes.Dto.RequestDto.GenreDTO;
using WeedTunes.Dto.RequestDto.SongDTOs;
using WeedTunes.Dto.ResponseDto.GenreDTO;
using WeedTunes.Utilities.Helpers;

namespace WeedTunes.Services.Interface
{
    public interface IGenreService
    {
        Task<BaseResponse<bool>> CreateGenre(CreateGenreDTO model);
        Task<BaseResponse<bool>> AddGenreToSong(AddGenreToSongDTO model); 
        Task<BaseResponse<List<GenreDTO>>> GetAllGenres(); 
    }
}
