using System;
using WeedTunes.Entities;

namespace WeedTunes.Dto.ResponseDto.SongDTOs
{
    public class SongDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public DateTime ReleasedDate { get; set; }
        public DateTime CreatedOn { get; set; }


        public static implicit operator SongDTO(Song model)
        {
            return model is null ? null
               : new SongDTO
               {
                   Id = model.Id,
                   Name = model.Name,
                   Artist = model.Artist,
                   ReleasedDate = model.ReleasedDate,
                   CreatedOn = model.CreatedOn
               };
        }

    }
}
