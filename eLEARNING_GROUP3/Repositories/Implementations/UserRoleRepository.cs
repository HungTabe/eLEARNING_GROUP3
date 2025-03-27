
﻿using Microsoft.EntityFrameworkCore;
using OnlineLearning.Data;
using OnlineLearning.Enums;

using OnlineLearning.Models.Domains.UserModels;
using OnlineLearning.Repositories.Interfaces;

namespace OnlineLearning.Repositories.Implementations
{
    public class UserRoleRepository : BaseRepository<UserRole>, IUserRoleRepository
    {
        public UserRoleRepository (OnlLearnDBContext context) : base(context) { }

        public async Task<List<RoleType>> GetRolesByUserIdAsync(long? userId)
        {
            return await _context.UserRoles
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.Role.RoleName)
                .ToListAsync();
        }
    }
}
