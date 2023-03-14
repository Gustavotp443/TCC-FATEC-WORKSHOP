using TCCFatecWorkshop.Models.DTO.Workshop;
using TCCFatecWorkshop.Models;

namespace TCCFatecWorkshop.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAll(int workshopId);
        Task<Product> FindById(int userId, int workshopId);
        Task<Product> Create(int userId, Product product);
        Task<Product> Update(int userId, Product product, int workshopId);
        Task<bool> Delete(int userId, int workshopId);
    }
}
