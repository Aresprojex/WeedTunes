using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeedTunes.Entities
{
    public class Strain : BaseEntity
    {
        public string Name { get; set; }

        public IEnumerable<StrainFeeling> Feelings { get; set; }

        public IEnumerable<StrainNegativeAspect> NegativeAspects { get; set; }

        public IEnumerable<StrainHelpsWith> HelpsWith { get; set; }

        public IEnumerable<StrainFlavour> Flavours { get; set; }
    }
}
