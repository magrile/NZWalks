using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public interface IWalkDifficultyRepository
    {

        public Task<IEnumerable<WalkDifficulty>> GetWalkDifficulties();

        // Segun ID

        public Task<WalkDifficulty> GetWalkDifficultyById(Guid walkDifficultyId);

        // Añadir walk Difficulty

        public Task<WalkDifficulty> AddWalkDifficulty(WalkDifficulty walkDifficulty);

        // Updatear walk

        public Task<WalkDifficulty> UpdateWalkDifficulty(Guid walkDifficultyId, WalkDifficulty walkDifficulty);

        // Delete

        public Task<WalkDifficulty> DeleteWalkDifficulty(Guid walkDifficultyId);
    }
}
