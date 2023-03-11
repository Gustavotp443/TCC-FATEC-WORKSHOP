using TCCFatecWorkshop.Models;

namespace TCCFatecWorkshop.Repositories.Interfaces
{
    public interface IWorkshopRepository
    {
        Task<Workshop> FindById(int userId, int workshopId);
        Task<Workshop> Create(int userId, Workshop workshop);
        Task<Workshop> Update(int userId, Workshop workshop, int workshopId);
        Task<bool> Delete(int userId, int workshopId);
    }
}
