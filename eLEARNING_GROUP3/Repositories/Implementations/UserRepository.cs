using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data;
using OnlineLearning.Models.Domains.Miscellaneous;
using OnlineLearning.Models.Domains.UserModels;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Repositories.Interfaces;

namespace OnlineLearning.Repositories.Implementations
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(OnlLearnDBContext context) : base(context) { }

        public async Task<bool> UpdateAvatarAsync(long id, string url)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return false;
            }

            // Update info
            existingUser.AvatarUrl = url;

            // Save
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> UpdateUserInfoAsync(ProfileDTO profile)
        {
            var existingUser = await _context.Users.FindAsync(profile.UserId);
            if (existingUser == null)
            {
                return false;
            }

            // Update info
            existingUser.FullName = profile.FullName;
            existingUser.Phone = profile.Phone;
            existingUser.Gender = profile.Gender;
            existingUser.Email = profile.Email;

            // Save
            _context.Users.Update(existingUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> GetByEmailAndPassword(string email, string password)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password
                                                && u.IsDeleted == false && u.IsActived == true);
            //&& u.IsDeleted == false && u.IsActived == true);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByPhoneAsync(string phone)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Phone.Equals(phone));
        }

        public async Task<List<User>> GetAllUsersWithRolesAsync()
        {
            return await _context.Users
            .Include(u => u.UserRoles) // Lấy UserRoles
            .ThenInclude(ur => ur.Role) // Lấy Role từ UserRole
            .ToListAsync();
        }

        public async Task<User?> GetUsersWithRolesAsync(long id)
        {
            return await _context.Users
            .Include(u => u.UserRoles) // Lấy UserRoles
            .ThenInclude(ur => ur.Role) // Lấy Role từ UserRole
            .FirstOrDefaultAsync(u => u.UserId == id);
        }
      
    }
}