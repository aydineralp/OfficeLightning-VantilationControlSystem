namespace CRUDify_API.Repositories.Abstract
{
    public interface IAuthRepository
    {
        bool LoginWithEmailAndPassword(string email, string password);
        bool CheckUserByEmail(string email);
    }
}
