using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeedTunes.Data;
using WeedTunes.Dto.RequestDto.SongDTOs;
using WeedTunes.Dto.ResponseDto.SongDTOs;
using WeedTunes.Dto.ResponseDto.UserPlayListDTO;
using WeedTunes.Entities;
using WeedTunes.Services.Interface;
using WeedTunes.Utilities;
using WeedTunes.Utilities.Helpers;
using WeedTunes.Utilities.Pagination;

namespace WeedTunes.Services.Implementation
{
    public class SongService : ISongService
    {
        private readonly ApplicationDbContext _dbContext;

        public SongService
        (
            ApplicationDbContext dbContext
        )
        {
            _dbContext = dbContext; 
        }

        public async Task<BaseResponse<bool>> AddStrainToSong(AddStrainToSongDTO model)
        {
            var songExist = await _dbContext.Songs.FirstOrDefaultAsync(x => x.Id == model.SongId);

            if (songExist is null)
            {
                return new BaseResponse<bool>("Song doesn't exist", new List<string> { "Song doesn't exist" });
            }

            var strainExist = await _dbContext.Strains.FirstOrDefaultAsync(x => x.Id == model.StrainId);

            if (strainExist is null)
            {
                return new BaseResponse<bool>("Strain doesn't exist", new List<string> { "strain doesn't exist" });
            }

            if (songExist.StrainId != null)
            {
                return new BaseResponse<bool>("Song already has a strain", new List<string> { "Song already has a strain" });
            }

            songExist.StrainId = model.StrainId;

            _dbContext.Songs.Update(songExist);
            await _dbContext.SaveChangesAsync();

            return new BaseResponse<bool>(true, "Successfully added a strain to a song");
        }

        public async Task<BaseResponse<bool>> CreateSong(CreateSongDTO model)
        {
            var songExist = await _dbContext.Songs.FirstOrDefaultAsync(x => x.Name.ToLower().Replace(" ", "") == model.Name.ToLower().Replace(" ", ""));

            if (songExist != null)
            {
                return new BaseResponse<bool>($"Song with name {songExist.Name} already exist", new List<string> { $"Song with name {songExist.Name} already exist" });
            }

            var newSong = new Song
            {
                Name = model.Name,
                Artist = model.Artist,
                ReleasedDate = model.ReleasedDate
            };

            await _dbContext.Songs.AddAsync(newSong);
            await _dbContext.SaveChangesAsync();

            return new BaseResponse<bool>(true, "Successfully created a Song");
        }

        public async Task<BaseResponse<List<SongDTO>>> GetAllSongs() 
        {
            var songs = await _dbContext
                                  .Songs
                                  .OrderByDescending(x => x.CreatedOn)
                                  .ToListAsync();

            var songsDTOs = songs.Select(x => (SongDTO)x).ToList();

            return new BaseResponse<List<SongDTO>> { Data = songsDTOs, TotalCount = songsDTOs.Count, ResponseMessage = $"Found {songsDTOs.Count()} songs" };

        }

        public async Task<BaseResponse<PagedList<SongDTO>>> GetAllSongs(SongQueryDTO model)
        {

            var songs = await _dbContext
                                .Songs
                                .OrderByDescending(x => x.CreatedOn)
                                .ToListAsync();


            var pagedListOfSongs = songs.ToPagedList(model.PageIndex, model.PageSize);
            var songsDTOs = pagedListOfSongs.Select(x => (SongDTO)x).ToList();

            var data = new PagedList<SongDTO>(songsDTOs, model.PageIndex, model.PageSize, pagedListOfSongs.TotalItemCount);
            return new BaseResponse<PagedList<SongDTO>> { Data = data, TotalCount = data.TotalItemCount, ResponseMessage = $"Found {songsDTOs.Count} songs." };
        }
    }
}
