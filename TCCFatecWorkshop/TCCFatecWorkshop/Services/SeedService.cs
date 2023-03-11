using TCCFatecWorkshop.Data;
using TCCFatecWorkshop.Models;
using TCCFatecWorkshop.Models.Enums;

namespace TCCFatecWorkshop.Services
{
    public class SeedService
    {
        private readonly WorkshopProjectDBContext _dbContext;

        public SeedService(WorkshopProjectDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void SeedDataContext()
        {
            /*if(!_dbContext.Users.Any())
            {
            }*/
        }
    }
}
