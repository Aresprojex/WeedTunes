using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeedTunes.Entities
{
    public class StrainHelpsWith : BaseEntity
    {
        public string Name { get; set; }
        public Strain Strain { get; set; }
        public Guid StrainId { get; set; }
    }
}
