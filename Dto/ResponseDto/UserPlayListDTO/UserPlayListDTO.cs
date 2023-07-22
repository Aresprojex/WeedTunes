using System;
using WeedTunes.Entities;

namespace WeedTunes.Dto.ResponseDto.UserPlayListDTO
{
    public class UserPlayListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateCreated { get; set; }

        public static implicit operator UserPlayListDTO(UserPlayList model)
        {
            return model is null ? null
               : new UserPlayListDTO
               {
                   Id = model.Id,
                   Name = model.Name,
                   DateCreated = model.CreatedOn
               };
        }
    }
}
