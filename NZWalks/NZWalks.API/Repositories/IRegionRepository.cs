using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IRegionRepository
    {
        //// Metodo sincrono
        //IEnumerable<Region> GetAllRegions();

        // Metodo Asincrono ( muchas peticiones a la vez )
        Task<IEnumerable<Region>> GetAllRegionsAsync();

        // Devolcer una region por su Id
        Task<Region> GetRegionByIdAsync(Guid regionId);

        // Devolver una region por su nombre
        Task<IEnumerable<Region>> GetRegionByNameAsync(string regionName);

        // Añadir una region

        Task<Region> AddRegionAsync(Region region);

        // Borrar una region
        Task<Region> DeleteRegionAsync(Guid regionId);

        // Update una region
        Task<Region> UpdateRegionAsync(Guid regionId, Region region);
        

    }
}
