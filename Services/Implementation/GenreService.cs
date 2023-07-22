using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeedTunes.Data;
using WeedTunes.Dto.RequestDto.GenreDTO;
using WeedTunes.Dto.RequestDto.SongDTOs;
using WeedTunes.Dto.ResponseDto.GenreDTO;
using WeedTunes.Dto.ResponseDto.SongDTOs;
using WeedTunes.Entities;
using WeedTunes.Services.Interface;
using WeedTunes.Utilities.Helpers;

namespace WeedTunes.Services.Implementation
{
    public class GenreService : IGenreService
    {
        private readonly ApplicationDbContext _dbContext;

        public GenreService
        (
            ApplicationDbContext dbContext
        )
        {
            _dbContext = dbContext;
        }

        public async Task<BaseResponse<bool>> AddGenreToSong(AddGenreToSongDTO model)
        {
            var songExist = await _dbContext.Songs.FirstOrDefaultAsync(x => x.Id == model.SongId);

            if (songExist is null)
            {
                return new BaseResponse<bool>("Song doesn't exist", new List<string> { "Song doesn't exist" });
            }

            var genreExist = await _dbContext.Genres.FirstOrDefaultAsync(x => x.Id == model.GenreId);

            if (genreExist is null)
            {
                return new BaseResponse<bool>("Genre doesn't exist", new List<string> { "Genre doesn't exist" });
            }

            if (songExist.GenreId != null)
            {
                return new BaseResponse<bool>("Song already has a genre", new List<string> { "Song already has a genre" });
            }

            songExist.GenreId = model.GenreId;

            _dbContext.Songs.Update(songExist);
            await _dbContext.SaveChangesAsync();

            return new BaseResponse<bool>(true, "Successfully added a genre to a song");
        }

        public async Task<BaseResponse<bool>> CreateGenre(CreateGenreDTO model)
        {
            var genreExist = await _dbContext.Genres.FirstOrDefaultAsync(x => x.Name.ToLower().Replace(" ", "") == model.Name.ToLower().Replace(" ", ""));

            if (genreExist != null)
            {
                return new BaseResponse<bool>($"Genre with name {genreExist.Name} already exist", new List<string> { $"Genre with name {genreExist.Name} already exist" });
            }

            var newGenre = new Genre
            {
                Name = model.Name,
            };

            await _dbContext.Genres.AddAsync(newGenre);
            await _dbContext.SaveChangesAsync();

            return new BaseResponse<bool>(true, "Successfully created a genre");
        }

        public async Task<BaseResponse<List<GenreDTO>>> GetAllGenres()
        {
            var genres = await _dbContext
                                    .Genres
                                    .OrderByDescending(x => x.CreatedOn)
                                    .ToListAsync();


            var genreDTOs = genres.Select(x => (GenreDTO)x).ToList();

            return new BaseResponse<List<GenreDTO>> { Data = genreDTOs, TotalCount = genreDTOs.Count, ResponseMessage = $"Found {genreDTOs.Count()} genre(s)" };
        }
    }
}
