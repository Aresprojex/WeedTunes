using System;
using WeedTunes.Dto.ResponseDto.SongDTOs;
using WeedTunes.Dto.ResponseDto.UserPlayListDTO;
using WeedTunes.Entities;

namespace WeedTunes.Dto.ResponseDto.UserFavouriteSongDTO
{
    public class UserFavouriteSongDTO
    {
        public Guid Id { get; set; }
        public SongDTO SongDTO { get; set; }
        public DateTime DateCreated { get; set; }


        public static implicit operator UserFavouriteSongDTO(UserFavouriteSong model)
        {
            return model is null ? null
               : new UserFavouriteSongDTO
               {
                   Id = model.Id,
                   SongDTO = model.Song,
                   DateCreated = model.CreatedOn
               };
        }
    }
}
