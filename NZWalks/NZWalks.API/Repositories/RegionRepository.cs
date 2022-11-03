using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class RegionRepository : IRegionRepository
    {

        // Creamos la propiedad de el contexto de nuestra base de datos
        private readonly NZWalksDbContext _nZWalksDbContext;

        // Creamos la el constructor con la inyeccion de nuestro contexto de la base de datos
        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this._nZWalksDbContext = nZWalksDbContext;
        }

       



        // Metodo sincrono
        //public IEnumerable<Region> GetAllRegions()
        //{

        //    var listaRegiones = _nZWalksDbContext.Regions.ToList();


        //    return listaRegiones;

        //}

        // Metodo asincrono

        public async Task<IEnumerable<Region>> GetAllRegionsAsync()
        {
            var listaRegiones = await _nZWalksDbContext.Regions.ToListAsync();


              return listaRegiones;

        }

        public async Task<Region> GetRegionByIdAsync(Guid regionId)
        {
            var regionSegunId = await _nZWalksDbContext.Regions.Where(region => region.Id == regionId).FirstOrDefaultAsync();

            return regionSegunId;
        }

        public async Task<IEnumerable<Region>> GetRegionByNameAsync(string regionName)
        {
            var regionSegunNombre = await _nZWalksDbContext.Regions.Where(region => region.Name == regionName).ToListAsync();

            return regionSegunNombre;
        }

        public async Task<Region> AddRegionAsync(Region region)
        {
            // Creamos una ID nueva automaticamente

            region.Id = Guid.NewGuid();

            // Añadimos la region a la db

             await _nZWalksDbContext.AddAsync(region);

            // Guardamos los cambios de la nueva region

            await _nZWalksDbContext.SaveChangesAsync();

            // Devolvemos la region añadida

            return region;

        }

        public async Task<Region> DeleteRegionAsync(Guid regionId)
        {
            var searchId = await _nZWalksDbContext.Regions.Where(region => region.Id == regionId).FirstOrDefaultAsync();

            if(searchId == null)
            {
                return null;
            }

             _nZWalksDbContext.Regions.Remove(searchId);

            await _nZWalksDbContext.SaveChangesAsync();

            return searchId;
        }

        public async Task<Region> UpdateRegionAsync(Guid regionId, Region region)
        {
            var buscarRegion = await _nZWalksDbContext.Regions.Where(region => region.Id == regionId).FirstOrDefaultAsync();

            if(buscarRegion == null)
            {
                return null;
            }

            buscarRegion.Code = region.Code;
            buscarRegion.Name = region.Code;
            buscarRegion.Area = region.Area;
            buscarRegion.Lat = region.Lat;
            buscarRegion.Long = region.Long;
            buscarRegion.Population = region.Population;

           await _nZWalksDbContext.SaveChangesAsync();

            return buscarRegion;
        }
    }
}
