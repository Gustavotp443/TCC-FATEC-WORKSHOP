using Microsoft.EntityFrameworkCore;
using TCCFatecWorkshop.Data;
using TCCFatecWorkshop.Models;
using TCCFatecWorkshop.Repositories.Interfaces;

namespace TCCFatecWorkshop.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WorkshopProjectDBContext _context;

        public UserRepository(WorkshopProjectDBContext context) 
        {
            _context = context;
        }


        public async Task<User> FindById(int id)
        {
            var item = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) throw new Exception($"User for ID:{id} not found");
            return item;
        }

        public async Task<User> Create(User user)  
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            return user;
        }

        public async Task<User> Update(User user, int id)
        {
           var item = await FindById(id);
           item.Password = user.Password;
           item.Username = user.Username;

           _context.Update(item);
            await _context.SaveChangesAsync();

            return item;

        }

        public async Task<bool> Delete(int id)
        {
            var item = await FindById(id);
            _context.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

       
    }
}
