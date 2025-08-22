using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IWalksRepository
    {
        Task<Walk> AddWalk(Walk walk);

        Task<List<Walk>> GetAllWalks(
            string? filterOn = null,
            string? filterQuery=null,
            string? sortBy = null,
            bool isAscending = true,
            int pageNumber = 1,
            int pageSize = 10);

        Task<Walk?> GetWalk(Guid id);
        Task<Walk?> UpdateWalkById(Guid id, Walk walkDomainModel);
        Task<Walk?> DeleteWalkById(Guid id); 
    }
}
