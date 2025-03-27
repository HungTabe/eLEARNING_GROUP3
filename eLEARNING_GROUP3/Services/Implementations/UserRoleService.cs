using OnlineLearning.Enums;
using OnlineLearning.Models.Domains.UserModels;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Services.Implementations
{
    public class UserRoleService : IUserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task<UserRole> AddUserRoleAsync(UserRole userRole)
        {
            return await _userRoleRepository.AddAsync(userRole);
        }

        public async Task<IEnumerable<RoleType>> GetRolesByUserIdAsync(long userId)
        {
            return await _userRoleRepository.GetRolesByUserIdAsync(userId);
        }
    }
}
