using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IWalksRepository
    {
        Task<Walk> AddWalk(Walk walk);
        Task<List<Walk>> GetAllWalks();
        Task<Walk?> GetWalk(int id);
    }
}
