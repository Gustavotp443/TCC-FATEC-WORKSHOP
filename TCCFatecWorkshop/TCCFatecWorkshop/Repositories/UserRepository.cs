using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TCCFatecWorkshop.Data;
using TCCFatecWorkshop.Models;
using TCCFatecWorkshop.Repositories.Exceptions;
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

        private static void ConvertToHashPassword(User user)
        {
            var passwordHasher = new PasswordHasher<User>();
            user.Password = passwordHasher.HashPassword(user, user.Password);
        }

        public async Task<bool> ValidatePassword(User user)
        {
            var userConsulted = await _context.FindAsync<User>(user.Username);
            if (userConsulted == null) return false;

            var passwordHasher = new PasswordHasher<User>();    //user , hashpass, pass
            var status = passwordHasher.VerifyHashedPassword(user, userConsulted.Password, user.Password);
            switch (status)
            {
                case PasswordVerificationResult.Failed: return false;
                case PasswordVerificationResult.Success: return true;
                case PasswordVerificationResult.SuccessRehashNeeded:
                    await Update(user, user.Id);
                    return true;
                default: throw new InvalidOperationException("Operação inválida");

            }
           
        }



        public async Task<User> FindById(int id)
        {
            var item = await _context.Users.FirstOrDefaultAsync(x => x.Id == id) ?? throw new NotFoundException($"User for ID:{id} not found");
            return item;
        }

        public async Task<User> Create(User user)  
        {
            if(await _context.Users.AnyAsync(u => u.Email == user.Email))
            {
                throw new EmailAlreadyExistsException($"E-mail {user.Email} already exist");
            }
            if (await _context.Users.AnyAsync(u => u.Username == user.Username))
            {
                throw new UsernameAlreadyExistsException($"Username {user.Username} already exist");
            }

            ConvertToHashPassword(user);
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            
            return user;
        }

        public async Task<User> Update(User user, int id)
        {
           var item = await FindById(id) ?? throw new NotFoundException($"User for ID:{id} not found");
            if (user.Password != null) {
                ConvertToHashPassword(user);
                item.Password = user.Password;
            }
            
           if (user.Username != null) item.Username = user.Username;

           _context.Update(item);
            await _context.SaveChangesAsync();

            return item;

        }

        public async Task<bool> Delete(int id)
        {
            var item = await FindById(id) ?? throw new NotFoundException($"User for ID:{id} not found");
            _context.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User>  GetByEmailAndPassword(string email, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email) ?? throw new LoginFailed("Email or password invalid, please verify");
            var passwordHasher = new PasswordHasher<User>();
            var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, password);

            if (passwordVerificationResult == PasswordVerificationResult.Success)
            {
                return user;
            }
            else
            {
                throw new LoginFailed("Email or password invalid, please verify");
            }
        }

    }
}
