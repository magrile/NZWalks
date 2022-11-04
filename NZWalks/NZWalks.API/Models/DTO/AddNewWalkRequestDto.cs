using NZWalks.API.Models.Domain;

namespace NZWalks.API.Models.DTO
{
    public class AddNewWalkRequestDto
    {

        public string Name { get; set; }
        public double Length { get; set; }
        
        public Guid RegionId { get; set; }
        public Guid WalkDifficultyId { get; set; }

        
    }
}
