using System.Collections.Generic;

namespace WeedTunes.Dto.RequestDto
{
    public class CreateStrianDto
    {
        public string Name { get; set; }

        public IEnumerable<StrainFeelingDto> Feelings { get; set; }

        public IEnumerable<StrainNegativeAspectDto> NegativeAspects { get; set; }

        public IEnumerable<StrainHelpsWithDto> HelpsWith { get; set; }

        public IEnumerable<StrainFlavourDto> Flavours { get; set; }
    }

    public class StrainFeelingDto
    {
        public string Name { get; set; }
    }

    public class StrainNegativeAspectDto
    {
        public string Name { get; set; }
    }

    public class StrainHelpsWithDto
    {
        public string Name { get; set; }
    }
    
    public class StrainFlavourDto
    {
        public string Name { get; set; }
    }
}
