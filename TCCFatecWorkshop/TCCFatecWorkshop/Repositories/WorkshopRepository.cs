using Microsoft.EntityFrameworkCore;
using TCCFatecWorkshop.Data;
using TCCFatecWorkshop.Models;
using TCCFatecWorkshop.Models.DTO.Workshop;
using TCCFatecWorkshop.Repositories.Exceptions;
using TCCFatecWorkshop.Repositories.Interfaces;

namespace TCCFatecWorkshop.Repositories
{
    public class WorkshopRepository : IWorkshopRepository
    {
        private readonly WorkshopProjectDBContext _context;
        private readonly IUserRepository _userRepository;
        public WorkshopRepository(WorkshopProjectDBContext context, IUserRepository userRepository) 
        {
            _context = context;
            _userRepository= userRepository;
        }

        private async Task NegateAccess(int userId, int workshopId)
        {
            if (!await _context.Workshops.AnyAsync(w => w.UserId == userId && w.Id == workshopId)) throw new NotFoundException($"You cannot access this Workshop");
        }

        public async Task<Workshop> Create(int userId, Workshop workshop)
        {
            var user = await _userRepository.FindById(userId);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            if (await _context.Workshops.AnyAsync(w => w.UserId == userId && w.Name == workshop.Name))
            {
                throw new NameAlreadyExistsException($"{workshop.Name} already exists for this user");
            }

            workshop.UserId = userId;
            user.Workshops.Add(workshop);
            await _context.Workshops.AddAsync(workshop);
            await _userRepository.Update(user, userId);

            await _context.SaveChangesAsync();

            return workshop;
        }

        public async Task<bool> Delete(int userId, int workshopId)
        {
            await NegateAccess(userId, workshopId);
            
            var item = await FindById(userId, workshopId) ?? throw new NotFoundException($"Workshop for ID:{workshopId} not found");
            _context.Remove(item);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<WorkshopGetDTO> FindByIdDTO(int userId, int workshopId)
        {
            await NegateAccess(userId, workshopId);
            WorkshopGetDTO item = await _context
                .Workshops
                .Select(w => new WorkshopGetDTO
                {
                    Id = w.Id,
                    Name = w.Name,
                    Email = w.Email,
                    Description = w.Description,
                    UserId = w.UserId,
                    CreatedAt = w.CreatedAt,
                    UpdatedAt = w.UpdatedAt
                })
                .FirstOrDefaultAsync(x => x.Id == workshopId) ?? throw new NotFoundException($"Workshop for ID:{workshopId} not found");
            return item;
        }

        public async Task<Workshop> FindById(int userId, int workshopId)
        {
            await NegateAccess(userId, workshopId);
            var item = await _context
                .Workshops
                .FirstOrDefaultAsync(x => x.Id == workshopId) ?? throw new NotFoundException($"Workshop for ID:{workshopId} not found");
            return item;
        }

        public async Task<Workshop> Update(int userId, Workshop workshop, int workshopId)
        {
            await NegateAccess(userId, workshopId);

            var existingWorkshop = await _context.Workshops.FindAsync(workshopId) ?? throw new NotFoundException($"Workshop with ID {workshopId} not found");
            existingWorkshop.Name = workshop.Name;
            existingWorkshop.Email = workshop.Email;
            existingWorkshop.Description = workshop.Description;

            await _context.SaveChangesAsync();
            return existingWorkshop;

        }

        public async Task<List<WorkshopGetDTO>> GetAll(int userId)
        {
            return await _context.Workshops.Where(x => x.UserId == userId)
                .Select(x => new WorkshopGetDTO {
                    Id = x.Id,
                    Name = x.Name,
                    Email = x.Email,
                    Description = x.Description,
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                })
                .ToListAsync();
        }
    }
}
