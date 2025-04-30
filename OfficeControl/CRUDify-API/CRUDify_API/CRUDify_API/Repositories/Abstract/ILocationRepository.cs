using CRUDify_API.Entities;

namespace CRUDify_API.Repositories.Abstract
{
    public interface ILocationRepository
    {
        Task<IEnumerable<Location>> GetAllLocations();
    }
}
