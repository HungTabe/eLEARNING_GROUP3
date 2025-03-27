using OnlineLearning.Models.Domains.UserModels;
using OnlineLearning.Models.DTOs;
namespace OnlineLearning.Services.Interfaces.Admin
{
    public interface IUserManagementService
    {
        Task AddUser(Admin_AddUserDto model);
        Task UpdateUser(Admin_AddUserDto model);
        Task DeleteUser(long id);

        Task<List<Role>> GetAllRole();

        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUserAndRolesAsync(long userId);
    }
}
