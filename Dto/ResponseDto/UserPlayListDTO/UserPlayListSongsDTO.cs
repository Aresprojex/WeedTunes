using System;
using WeedTunes.Dto.ResponseDto.SongDTOs;
using WeedTunes.Entities;

namespace WeedTunes.Dto.ResponseDto.UserPlayListDTO
{
    public class UserPlayListSongsDTO
    {
        public Guid Id { get; set; }
        public SongDTO SongDTO { get; set; }
        public DateTime DateCreated { get; set; } 

        public static implicit operator UserPlayListSongsDTO(UserPlayListSong model) 
        {
            return model is null ? null
               : new UserPlayListSongsDTO
               {
                   Id = model.Id,
                   SongDTO = model.Song,
                   DateCreated = model.CreatedOn
               };
        }
    }
}
