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
    }
}
