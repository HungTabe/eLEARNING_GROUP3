
﻿using OnlineLearning.Enums;
using OnlineLearning.Models.Domains.UserModels;


namespace OnlineLearning.Repositories.Interfaces
{
    public interface IUserRoleRepository : IBaseRepository<UserRole>
    {
        Task<List<RoleType>> GetRolesByUserIdAsync(long? userId);

    }
}
