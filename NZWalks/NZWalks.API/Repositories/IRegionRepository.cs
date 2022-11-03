using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        //// Metodo sincrono
        //IEnumerable<Region> GetAllRegions();

        // Metodo Asincrono ( muchas peticiones a la vez )
        Task<IEnumerable<Region>> GetAllRegionsAsync();
    }
}
