using AutoMapper;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Profiles
{
    public class RegionsProfile: Profile
    {
        // Creamos el constructor de nuestro profile
        public RegionsProfile()
        {
            CreateMap<Region, RegionDto>()
                .ReverseMap();


        }
    }
}
