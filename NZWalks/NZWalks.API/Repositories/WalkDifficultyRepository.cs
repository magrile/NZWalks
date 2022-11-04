using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext _nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this._nZWalksDbContext = nZWalksDbContext;
        }

       

        public async Task<IEnumerable<WalkDifficulty>> GetWalkDifficulties()
        {
            var listaDificultad = await _nZWalksDbContext.WalkDifficulty.ToListAsync();

            if(listaDificultad == null)
            {
                return null;
            }

            return listaDificultad;
        }

        public async Task<WalkDifficulty> GetWalkDifficultyById(Guid walkDifficultyId)
        {
            var getWalkDificultyId = await _nZWalksDbContext.WalkDifficulty.Where(walkDifficulty => walkDifficulty.Id == walkDifficultyId).FirstOrDefaultAsync();

            if(getWalkDificultyId == null)
            {
                return null;
            }

            return getWalkDificultyId;
        }


        public async Task<WalkDifficulty> AddWalkDifficulty(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id =  Guid.NewGuid();

           await _nZWalksDbContext.WalkDifficulty.AddAsync(walkDifficulty);

            await _nZWalksDbContext.SaveChangesAsync();

            return walkDifficulty;
        }

        public async Task<WalkDifficulty> UpdateWalkDifficulty(Guid walkDifficultyId, WalkDifficulty walkDifficulty)
        {
            var existingWalkDifficulty = await _nZWalksDbContext.WalkDifficulty.Where(wf => wf.Id == walkDifficultyId).FirstOrDefaultAsync();
        
            if(existingWalkDifficulty == null)
            {
                return null;
            }


            existingWalkDifficulty.Code = walkDifficulty.Code;

            await _nZWalksDbContext.SaveChangesAsync();

            return existingWalkDifficulty;





        }

        public async Task<WalkDifficulty> DeleteWalkDifficulty(Guid walkDifficultyId)
        {
            var existingWalkDifficulty = await _nZWalksDbContext.WalkDifficulty.Where(wf => wf.Id == walkDifficultyId).FirstOrDefaultAsync();

            if (existingWalkDifficulty == null)
            {
                return null;
            }

             _nZWalksDbContext.WalkDifficulty.Remove(existingWalkDifficulty);
            await _nZWalksDbContext.SaveChangesAsync();

            return existingWalkDifficulty;


        }
    }
}
