using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeedTunes.Data;
using WeedTunes.Dto;
using WeedTunes.Dto.RequestDto.UserFavouriteSongDTO;
using WeedTunes.Dto.RequestDto.UserPlayListDTO;
using WeedTunes.Dto.ResponseDto.UserFavouriteSongDTO;
using WeedTunes.Dto.ResponseDto.UserPlayListDTO;
using WeedTunes.Entities;
using WeedTunes.Services.Interface;
using WeedTunes.Utilities.Helpers;
using WeedTunes.Utilities.Pagination;

namespace WeedTunes.Services.Implementation
{
    public class UserPlayListService : IUserPlayListService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserPlayListService
        (
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager
        )
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<BaseResponse<bool>> AddFavouriteSongsToUser(AddFavouriteSongsToUserDTO model)
        {
            var user = _userManager.FindByIdAsync(model.UserId.ToString());

            if (user == null)
            {
                return new BaseResponse<bool>("User doesn't exist", new List<string> { "User doesn't exist" });
            }

            //CHECK ALL SONGS
            if (!model.SongIds.Any())
            {
                return new BaseResponse<bool>("Song(s) to be added cannot be empty", new List<string> { "Song(s) to be added cannot be empty" });
            }

            var checkSongs = _dbContext.Songs.All(x => model.SongIds.Contains(x.Id));

            if (!checkSongs)
            {
                return new BaseResponse<bool>("Song(s) to be added to playlist doesn't exist", new List<string> { "Song(s) to be added to playlist doesn't exist" });
            }

            //CHECK IF THE USER HASN'T ADDED THE SONG TO HIS PLAYLIST
            var existingUserFavouriteSongs = await _dbContext
                                     .UserFavouriteSongs
                                     .Where(x => x.ApplicationUserId == model.UserId)
                                     .ToListAsync();

            foreach (var songId in model.SongIds)
            {
                if (existingUserFavouriteSongs.FirstOrDefault(x => x.SongId == songId) != null)
                {
                    return new BaseResponse<bool>("Song has already been added to your list of favourite song", new List<string> { "Song has already been added to your list of favourite song" });
                }
            }

            var userFavouriteSongsToBeAdded = new List<UserFavouriteSong>();
            model.SongIds.ForEach(x => userFavouriteSongsToBeAdded.Add(new UserFavouriteSong
            {
                SongId = x,
                ApplicationUserId = model.UserId
            }));

            await _dbContext.UserFavouriteSongs.AddRangeAsync(userFavouriteSongsToBeAdded);
            await _dbContext.SaveChangesAsync();

            return new BaseResponse<bool>(true, $"Successfully added {model.SongIds.Count()} song(s) to your favourite songs");

        }

        public async Task<BaseResponse<bool>> AddSongsToUserPlayList(AddSongsToUserPlayListDTO model, Guid userId)
        {
            var user = _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return new BaseResponse<bool>("User doesn't exist", new List<string> { "User doesn't exist" });
            }

            //CHECK ALL SONGS
            if (!model.SongIds.Any())
            {
                return new BaseResponse<bool>("Song(s) to be added cannot be empty", new List<string> { "Song(s) to be added cannot be empty" });
            }

            //CHECK IF USER HAS THE PLAY LIST HE WANTS TO ADD SONGS TO
            var playListExist = await _dbContext.UserPlayLists.FirstOrDefaultAsync(x => x.Id == model.PlayListId);

            if (playListExist is null)
            {
                return new BaseResponse<bool>("User playlist doesn't exist", new List<string> { "User playlist doesn't exist" });
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


            //CHECK IF THE USER HASN'T ADDED THE SONG TO HIS FAVOURITE SONG
            var userPlayList = await _dbContext
                                     .UserPlayLists
                                     .Where(x => x.ApplicationUserId == userId)
                                     .Include(x => x.UserPlayListSongs)
                                     .FirstOrDefaultAsync();

            foreach (var songId in model.SongIds)
            {
                if (userPlayList.UserPlayListSongs.FirstOrDefault(x => x.SongId == songId) != null)
                {
                    return new BaseResponse<bool>("Song has already been added to your playlist", new List<string> { "Song has already been added to your playlist" });
                }
            }

            model.SongIds.ForEach(x => userPlayList.UserPlayListSongs.Add(new UserPlayListSong
            {
                SongId = x,
                UserPlayListId = userPlayList.Id
            }));

            await _dbContext.SaveChangesAsync();
            return new BaseResponse<bool>(true, $"Successfully added {model.SongIds.Count()} song(s) to your playlist");

        }

        public async Task<BaseResponse<bool>> AddSongToUserPlayList(AddSongToUserPlayListDTO model) 
        {

            var userPlayListExist = await _dbContext.UserPlayLists.FirstOrDefaultAsync(x => x.Id == model.PlayListId);

            if (userPlayListExist is null)
            {
                return new BaseResponse<bool>("User playlist doesn't exist", new List<string> { "User playlist doesn't exist" });
            }

            var songExist = await _dbContext.Songs.FirstOrDefaultAsync(x => x.Id == model.SongId);

            if (songExist is null)
            {
                return new BaseResponse<bool>("Song to be added to playlist doesn't exist", new List<string> { "Song to be added to playlist doesn't exist" });
            }

            //CHECK IF THE USER HASN'T ADDED THE SONG TO HIS PLAYLIST
            var userPlayList = await _dbContext
                                     .UserPlayLists
                                     .Where(x => x.ApplicationUserId == model.UserId)
                                     .Include(x => x.UserPlayListSongs)
                                     .FirstOrDefaultAsync();

            if (userPlayList.UserPlayListSongs.FirstOrDefault(x => x.SongId == model.SongId) != null)
            {
                return new BaseResponse<bool>("Song has already been added to your playlist", new List<string> { "Song has already been added to your playlist" });
            }

            userPlayListExist.UserPlayListSongs.Add(new UserPlayListSong
            {
                SongId = model.SongId
            });

            await _dbContext.UserPlayLists.AddAsync(userPlayListExist);
            await _dbContext.SaveChangesAsync();

            return new BaseResponse<bool>(true, "Successfully added a song to your playlist");
        }

        public async Task<BaseResponse<bool>> CreatePlayList(CreateUserPlayListDTO model)
        {
            var user = _userManager.FindByIdAsync(model.UserId.ToString());

            if (user == null)
            {
                return new BaseResponse<bool>("User doesn't exist", new List<string> { "User doesn't exist" });
            }

            var playListExist = await _dbContext.UserPlayLists.FirstOrDefaultAsync(x => x.Name.ToLower().Replace(" ", "") == model.Name.ToLower().Replace(" ", ""));

            if (playListExist != null)
            {           
                return new BaseResponse<bool>($"User play list with name {playListExist.Name} already exist", new List<string> { $"User play list with name {playListExist.Name} already exist" });
            }

            var newUserPlayList = new UserPlayList
            {
                Name = model.Name,
                ApplicationUserId = model.UserId
            };

            await _dbContext.UserPlayLists.AddAsync(newUserPlayList);
            await _dbContext.SaveChangesAsync();

            return new BaseResponse<bool>(true, "Successfully created your playlist");
        }

        public async Task<BaseResponse<PagedList<UserFavouriteSongDTO>>> GetAUserFavouriteSongs(UserFavouriteSongQueryDTO model, Guid userId)
        {
            var userFavouriteSongs = await _dbContext
                                             .UserFavouriteSongs
                                             .Where(x => x.ApplicationUserId == userId)
                                             .Include(x => x.Song)
                                             .OrderByDescending(x => x.CreatedOn)
                                             .ToListAsync();

            var pagedUserFavouriteSongs = userFavouriteSongs.ToPagedList(model.PageIndex, model.PageSize);
            var userFavouriteSongsDTO = pagedUserFavouriteSongs.Select(x => (UserFavouriteSongDTO)x).ToList();

            var data = new PagedList<UserFavouriteSongDTO>(userFavouriteSongsDTO, model.PageIndex, model.PageSize, pagedUserFavouriteSongs.TotalItemCount);
            return new BaseResponse<PagedList<UserFavouriteSongDTO>> { Data = data, TotalCount = data.TotalItemCount, ResponseMessage = $"Found {userFavouriteSongsDTO.Count} favourites song." };
        }

        public async Task<BaseResponse<PagedList<UserPlayListDTO>>> GetAUserPlayLists(UserPlayListQueryDTO model, Guid userId)
        {
            var playLists = await _dbContext
                                      .UserPlayLists
                                      .Where(x => x.ApplicationUserId == userId)
                                      .OrderByDescending(x => x.CreatedOn)
                                      .ToListAsync();  

            var pagedplayLists = playLists.ToPagedList(model.PageIndex, model.PageSize);
            var playListDTO = pagedplayLists.Select(x => (UserPlayListDTO)x).ToList();

            var data = new PagedList<UserPlayListDTO>(playListDTO, model.PageIndex, model.PageSize, pagedplayLists.TotalItemCount);
            return new BaseResponse<PagedList<UserPlayListDTO>> { Data = data, TotalCount = data.TotalItemCount, ResponseMessage = $"Found {playListDTO.Count} Play list(s)." };
        }

        public async Task<BaseResponse<PagedList<UserPlayListSongsDTO>>> GetAUserPlayListSongs(UserPlayListSongQueryDTO model)
        {
            var userPlayListSongs = await _dbContext
                                             .UserPlayListSongs
                                             .Where(x => x.UserPlayListId == model.PlayListId)
                                             .Include(x => x.Song)
                                             .OrderByDescending(x => x.CreatedOn)
                                             .ToListAsync();

            var pagedUserPlayListSongs = userPlayListSongs.ToPagedList(model.PageIndex, model.PageSize);
            var userPlayListSongsDTOs = pagedUserPlayListSongs.Select(x => (UserPlayListSongsDTO)x).ToList(); 

            var data = new PagedList<UserPlayListSongsDTO>(userPlayListSongsDTOs, model.PageIndex, model.PageSize, pagedUserPlayListSongs.TotalItemCount);
            return new BaseResponse<PagedList<UserPlayListSongsDTO>> { Data = data, TotalCount = data.TotalItemCount, ResponseMessage = $"Found {userPlayListSongsDTOs.Count} song in the user play list." };
        }

        public async Task<BaseResponse<bool>> RemoveFavouriteSongsFromUser(RemoveFavouriteSongsFromUserDTO model, Guid userId)
        {
            var user = _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
            {
                return new BaseResponse<bool>("User doesn't exist", new List<string> { "User doesn't exist" });
            }

            //CHECK ALL SONGS
            if (!model.SongIds.Any())
            {
                return new BaseResponse<bool>("Song(s) to be removed cannot be empty", new List<string> { "Song(s) to be removed cannot be empty" });
            }

            var existingUserFavouriteSongs = await _dbContext
                                                     .UserFavouriteSongs
                                                     .Where(x => x.ApplicationUserId == userId)
                                                     .ToListAsync();

            foreach (var songId in model.SongIds)
            {

                if (existingUserFavouriteSongs.FirstOrDefault(x => x.SongId == songId) == null)
                {
                    return new BaseResponse<bool>("Song doesn't exist in your playlist", new List<string> { "Song doesn't exist in your playlist" });
                }
            }

            _dbContext.UserFavouriteSongs.RemoveRange(existingUserFavouriteSongs);
            return new BaseResponse<bool>(true, $"Successfully removed {model.SongIds.Count()} song(s) from your playlist");

        }

        public async Task<BaseResponse<bool>> RemoveSongFromUserPlayList(RemoveSongFromPlayListDTO model, Guid userId) 
        {
            var playListExist = await _dbContext
                                      .UserPlayLists
                                      .Where(x => x.ApplicationUserId == userId && x.Id == model.UserPlayListId)
                                      .Include(x => x.UserPlayListSongs)
                                      .FirstOrDefaultAsync();

            if (playListExist == null)
            {
                return new BaseResponse<bool>("Play list doesn't exist for this user.", new List<string> { "Play list doesn't exist for this user." });
            }

            var checkForSongInUserPlayList = playListExist.UserPlayListSongs.FirstOrDefault(x => x.SongId == model.SongId);

            if (checkForSongInUserPlayList is null)
            {
                return new BaseResponse<bool>("Song doesn't exist in your play list.", new List<string> { $"Song doesn't exist in your play list." });
            }

            _dbContext.UserPlayListSongs.Remove(checkForSongInUserPlayList);
            await _dbContext.SaveChangesAsync();

            return new BaseResponse<bool>(true, "Successfully removed a song from your playlist");

        }
    }
}
