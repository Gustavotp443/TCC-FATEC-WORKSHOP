using TCCFatecWorkshop.Models;

namespace TCCFatecWorkshop.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> FindById(int userId);
        Task<User> Create(User user);
        Task<User> Update(User user, int userId);
        Task<bool> Delete(int userId);
        Task<User> GetByEmailAndPassword(string email, string password);
    }
}
