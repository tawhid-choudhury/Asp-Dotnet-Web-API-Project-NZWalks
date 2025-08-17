using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IRegionsRepository
    {
        Task<List<Region>> GetAllRegionsAsync();
        Task<Region?> GetRegionByIdAsync(Guid id);
        Task<Region> AddRegionAsync(Region region);
        Task<Region?> UpdateRegionAsync(Guid id, Region region);
        Task<Region?> DeleteRegionAsync(Guid id);
    }
}
