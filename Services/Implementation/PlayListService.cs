using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeedTunes.Data;
using WeedTunes.Dto.RequestDto.PlayListDTO;
using WeedTunes.Dto.RequestDto.SongDTOs;
using WeedTunes.Dto.RequestDto.UserPlayListDTO;
using WeedTunes.Dto.ResponseDto.SongDTOs;
using WeedTunes.Dto.ResponseDto.UserPlayListDTO;
using WeedTunes.Entities;
using WeedTunes.Services.Interface;
using WeedTunes.Utilities.Helpers;
using WeedTunes.Utilities.Pagination;

namespace WeedTunes.Services.Implementation
{
    public class PlayListService : IPlayListService
    {
        private readonly ApplicationDbContext _dbContext;

        public PlayListService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BaseResponse<bool>> AddSongsToPlayList(AddSongsToPlayListDTO model) 
        {
           
            //CHECK IF USER HAS THE PLAY LIST HE WANTS TO ADD SONGS TO
            var songExist = await _dbContext.PlayLists.FirstOrDefaultAsync(x => x.Id == model.PlayListId);

            if (songExist is null)
            {
                return new BaseResponse<bool>("Playlist doesn't exist", new List<string> { "Playlist doesn't exist" });
            }

            //CHECK ALL SONGS
            var checkSongs = _dbContext.Songs.Where(x => model.SongIds.Contains(x.Id));

            foreach (var songId in model.SongIds)
            {

                if (checkSongs.FirstOrDefault(x => x.Id == songId) == null)
                {
                    return new BaseResponse<bool>("Song(s) to be added to playlist doesn't exist", new List<string> { "Song(s) to be added to playlist doesn't exist" });
                }
            }

            //CHECK IF THE USER HASN'T ADDED THE SONG TO HIS PLAYLIST
            var playLists = await _dbContext
                                     .PlayLists
                                     .Where(x => x.Id == model.PlayListId)
                                     .Include(x => x.Songs)
                                     .FirstOrDefaultAsync();

            foreach (var songId in model.SongIds)
            {
                if (playLists.Songs.FirstOrDefault(x => x.Id == songId) != null)
                {
                    return new BaseResponse<bool>("Song has already been added to the playlist", new List<string> { "Song has already been added to the playlist" });
                }
            }

            foreach (var song in playLists.Songs)
            {
                song.PlayListId = model.PlayListId;
                _dbContext.Songs.Update(song);
            }

            await _dbContext.SaveChangesAsync();
            return new BaseResponse<bool>(true, $"Successfully added {model.SongIds.Count()} song(s) to your playlist");
        }

        public async Task<BaseResponse<bool>> CreatePlayList(CreatePlayListDTO model)
        {
            var playListExist = await _dbContext.PlayLists.FirstOrDefaultAsync(x => x.Name.ToLower().Replace(" ", "") == model.Name.ToLower().Replace(" ", ""));

            if (playListExist != null)
            {
                return new BaseResponse<bool>($"Play list with name {playListExist.Name} already exist on this platform", new List<string> { $"Play list with name {playListExist.Name} already exist on this platform" });
            }

            var newPlayList = new PlayList
            {
                Name = model.Name,
            };

            await _dbContext.PlayLists.AddAsync(newPlayList);
            await _dbContext.SaveChangesAsync();

            return new BaseResponse<bool>(true, "Successfully created a playlist");
        }

        public async Task<BaseResponse<bool>> DeleteSongFromPlayList(DeletePlayListDTO model)
        {
            var playListExist = await _dbContext
                                    .PlayLists
                                    .FirstOrDefaultAsync(x => x.Id == model.PlayListId);

            if (playListExist is null)
            {
                return new BaseResponse<bool>("Play list doesn't exist.", new List<string> { "Play list doesn't exist." });
            }

            //I DID NOT DO CASCADE DELETE I.E. THE SONGS TIED TO THIS PLAY LIST WOULD STILL REMAIN ON THE SONG ENTITY
            _dbContext.PlayLists.Remove(playListExist);
            await _dbContext.SaveChangesAsync();

            return new BaseResponse<bool>(true, "Successfully deleted a playlist");
        }

        public async Task<BaseResponse<PagedList<SongDTO>>> GetSongsByPlayList(PlayListQueryDTO model)  
        {
            var playList = await _dbContext
                                        .PlayLists
                                        .Where(x => x.Id == model.PlayListId)
                                        .Include(x => x.Songs)
                                        .OrderByDescending(x => x.CreatedOn)
                                        .FirstOrDefaultAsync();

            #region SPOTIFY MOCK MODEL

            /*//TO DO-->MOCK SPOTIFY API CALL 
            var responseFromSpotifyListOfSongs = new List<SpotifyListOfSongsDTO>()
            {
                new SpotifyListOfSongsDTO
                {
                    Album = "Hillsong 2022",
                    Artist = "Hillsong",
                    Name = "Awake my Soul"
                },

                new SpotifyListOfSongsDTO
                {
                    Album = "Hillsong 2023",
                    Artist = "Hillsong",
                    Name = "All for Love The Father Gave"
                },
            };

            var pagedListOfSongsFromSpotify = responseFromSpotifyListOfSongs.ToPagedList(model.PageIndex, model.PageSize);
            var userPlayListSongsDTOs = pagedListOfSongsFromSpotify.ToList();*/

            #endregion

            var pagedListOfSongsFromSpotify = playList.Songs.ToPagedList(model.PageIndex, model.PageSize);
            var userPlayListSongsDTOs = pagedListOfSongsFromSpotify.Select(x=> (SongDTO)x).ToList();

            var data = new PagedList<SongDTO>(userPlayListSongsDTOs, model.PageIndex, model.PageSize, pagedListOfSongsFromSpotify.TotalItemCount);
            return new BaseResponse<PagedList<SongDTO>> { Data = data, TotalCount = data.TotalItemCount, ResponseMessage = $"Found {userPlayListSongsDTOs.Count} songs in a playlist." };
        }

        public async Task<BaseResponse<List<SongDTO>>> GetSongsByPlayList(Guid playListId)
        {
            var playList = await _dbContext
                                        .PlayLists
                                        .Where(x => x.Id == playListId)
                                        .Include(x => x.Songs)
                                        .OrderByDescending(x => x.CreatedOn)
                                        .FirstOrDefaultAsync();

            /*//TO DO-->MOCK SPOTIFY API CALL 
             var responseFromSpotifyListOfSongs = new List<SpotifyListOfSongsDTO>()
             {
                 new SpotifyListOfSongsDTO
                 {
                     Album = "Hillsong 2022",
                     Artist = "Hillsong",
                     Name = "Awake my Soul"
                 },

                 new SpotifyListOfSongsDTO
                 {
                     Album = "Hillsong 2023",
                     Artist = "Hillsong",
                     Name = "All for Love The Father Gave"
                 },
             };

             var pagedListOfSongsFromSpotify = responseFromSpotifyListOfSongs.ToPagedList(model.PageIndex, model.PageSize);
             var userPlayListSongsDTOs = pagedListOfSongsFromSpotify.ToList();*/

            var userPlayListSongsDTOs = playList.Songs.Select(x => (SongDTO)x).ToList();

            return new BaseResponse<List<SongDTO>> { Data = userPlayListSongsDTOs, TotalCount = userPlayListSongsDTOs.Count, ResponseMessage = $"Found {userPlayListSongsDTOs.Count()} songs in a playlist" };

        }

    }
}
