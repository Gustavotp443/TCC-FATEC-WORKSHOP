using TCCFatecWorkshop.Models;

namespace TCCFatecWorkshop.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> FindById(int id);
        Task<User> Create(User user);
        Task<User> Update(User user, int id);
        Task<bool> Delete(int id);
        Task<User> GetByEmailAndPassword(string email, string password);
    }
}
