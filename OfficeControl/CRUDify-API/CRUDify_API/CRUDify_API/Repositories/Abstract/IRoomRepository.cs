using CRUDify_API.Entities;

namespace CRUDify_API.Repositories.Abstract
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetRoomsByLocationName(string locationName);
    }
}
