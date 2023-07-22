using System;
using WeedTunes.Utilities;

namespace WeedTunes.Dto.RequestDto.UserPlayListDTO
{
    public class UserPlayListQueryDTO : BaseSearchViewModel
    {
    }

    public class UserPlayListSongQueryDTO : BaseSearchViewModel
    {
        public Guid PlayListId { get; set; }
    }


}
