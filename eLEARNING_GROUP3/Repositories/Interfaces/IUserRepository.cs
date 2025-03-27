using OnlineLearning.Models.Domains.UserModels;
using OnlineLearning.Models.DTOs;

namespace OnlineLearning.Repositories.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        //Đã kế thừa IBaseRepository, 
        //CRUD cơ bản sẵn, 
        //ae chỉ code những cái riêng của mỗi model thôi
        Task<User?> GetByEmailAndPassword(string email, string password);
        Task<User?> GetByPhoneAsync(string phone);

        Task<List<User>> GetAllUsersWithRolesAsync();
        Task<User> GetUsersWithRolesAsync(long userId);

        Task<User?> GetUserByEmail(string email);

		Task<bool> UpdateUserInfoAsync(ProfileDTO profile);
		Task<bool> UpdateAvatarAsync(long id, string url);

	}
}
