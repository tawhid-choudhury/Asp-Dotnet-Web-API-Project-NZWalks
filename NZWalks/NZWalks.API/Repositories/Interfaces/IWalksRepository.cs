using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IWalksRepository
    {
        Task<Walk> AddWalk(Walk walk);
        Task<List<Walk>> GetAllWalks();
        Task<Walk?> GetWalk(int id);
        Task<Walk?> UpdateWalkById(int id, Walk walkDomainModel);
        Task<Walk?> DeleteWalkById(int id); 
    }
}
