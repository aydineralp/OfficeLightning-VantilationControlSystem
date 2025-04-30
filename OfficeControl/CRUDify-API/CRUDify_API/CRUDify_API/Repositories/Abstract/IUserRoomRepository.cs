using CRUDify_API.Entities;

namespace CRUDify_API.Repositories.Abstract
{
    public interface IUserRoomRepository
    {
        IEnumerable<UserRoom> GetUserRoomByUserId(Guid userID);
        bool CheckUserRoomByCompositPK(Guid userID,Guid roomID);
    }
}
