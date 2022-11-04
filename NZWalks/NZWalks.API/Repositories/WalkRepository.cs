using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using System.Data.Common;

namespace NZWalks.API.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;

        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this._nZWalksDbContext = nZWalksDbContext;
        }

        


        // Obtener todos los paseos
        public async Task<IEnumerable<Walk>> GetAllWalksAsync()
        {
            var walk = await _nZWalksDbContext.Walks
                .Include(region => region.Region)
                .Include(dificulty => dificulty.WalkDifficulty)
                .ToListAsync();

            return walk;
        }

        public async Task<Walk> GetWalkById(Guid walkId)
        {
            var walkById = await _nZWalksDbContext.Walks
                .Include(region => region.Region)
                .Include(walkDif => walkDif.WalkDifficulty)
                   .Where(walk => walk.Id == walkId).FirstOrDefaultAsync();

            if(walkById == null)
            {
                return null;
            }

            return walkById;
        }

        public async Task<Walk> AddNewWalk(Walk walk)
        {
            walk.Id = Guid.NewGuid();

            await _nZWalksDbContext.Walks.AddAsync(walk);

            await _nZWalksDbContext.SaveChangesAsync();

            return walk;
        }
    }
}
