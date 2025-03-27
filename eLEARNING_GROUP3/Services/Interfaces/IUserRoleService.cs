using OnlineLearning.Models.Domains.UserModels;
using OnlineLearning.Enums;

namespace OnlineLearning.Services.Interfaces
{
    public interface IUserRoleService
    {
        Task<UserRole> AddUserRoleAsync(UserRole userRole);
        Task<IEnumerable<RoleType>> GetRolesByUserIdAsync(long userId);
    }
}
