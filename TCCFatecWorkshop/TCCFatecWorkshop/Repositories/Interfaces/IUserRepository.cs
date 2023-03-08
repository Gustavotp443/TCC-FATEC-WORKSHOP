using TCCFatecWorkshop.Models;

namespace TCCFatecWorkshop.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> FindById(int id);
        Task<User> Add(User user);
        Task<User> Update(User user, int id);
        Task<bool> Delete(int id);
    }
}
