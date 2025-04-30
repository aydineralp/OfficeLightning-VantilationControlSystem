using CRUDify_API.Entities;

namespace CRUDify_API.Repositories.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        Task<User> GetUserByEmail(string email);
    }
}
