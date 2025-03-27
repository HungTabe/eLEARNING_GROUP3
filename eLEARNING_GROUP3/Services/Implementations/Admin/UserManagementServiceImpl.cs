using Microsoft.CodeAnalysis.Scripting;
using NuGet.Protocol;
using OnlineLearning.Models.Domains.UserModels;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Interfaces;
using OnlineLearning.Services.Interfaces.Admin;
using System.Data;

namespace OnlineLearning.Services.Implementations.Admin
{
    public class UserManagementServiceImpl : IUserManagementService
    {
        private readonly IUserRepository _userRepo;
        private readonly IUserService _userService;
        private readonly IUserRoleRepository _userRoleRepo;
        private readonly IFileUploadService _fileUploadService; // Service upload ảnh
        private readonly IRoleRepository _roleRepository;
        public UserManagementServiceImpl( IUserService userService, IUserRepository userRepo, IFileUploadService fileUploadService, IUserRoleRepository userRoleRepo, IRoleRepository roleRepository)
        {
            _userService = userService;
            _userRepo = userRepo;
            _fileUploadService = fileUploadService;
            _userRoleRepo = userRoleRepo;
            _roleRepository = roleRepository;
        }
        public async Task AddUser(Admin_AddUserDto model)
        {
            var userbyEmail = await _userService.GetUserByEmailAsync(model.Email);
           
            if (userbyEmail != null) // Nếu email đã tồn tại, ném lỗi
            {
                throw new Exception("Email already exists");
            }
            var userByPhone = await _userRepo.GetByPhoneAsync(model.Phone);

            if (userByPhone != null) // Nếu email đã tồn tại, ném lỗi
            {
                throw new Exception("Phone already exists");
            }

            // Xử lý upload ảnh nếu có
            string avatarUrl = null;
            if (model.Avatar != null)
            {
                avatarUrl = await _fileUploadService.UploadFileAsync(model.Avatar);
            }
            // Tạo user mới từ DTO
            var newUser = new User
            {
                FullName = model.Fullname,
                Email = model.Email,
                Phone = model.Phone,
                AvatarUrl = avatarUrl ?? "default-avatar.png", // Nếu không có ảnh, dùng mặc định,
                Gender = model.Gender,
                Dob = model.Dob,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password) // Hash password trước khi lưu
            };


            try
            {
                await _userRepo.AddAsync(newUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner StackTrace: {ex.InnerException.StackTrace}");
                }
                throw;
            }

            // Gán Role cho User
            var userRole = new UserRole
            {
                UserId = newUser.UserId,
                RoleId = model.RoleId,
            };

           
            try
            {
                await _userRoleRepo.AddAsync(userRole);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding user role: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    Console.WriteLine($"Inner StackTrace: {ex.InnerException.StackTrace}");
                }
                throw;
            }

        }

        public async Task DeleteUser(long id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null)
            {
                throw new Exception("user Not Found");
            }
            user.IsActived = false;
            user.IsDeleted = true;
            await _userRepo.UpdateAsync(user);
        }

        public async Task<List<Role>> GetAllRole()
        {
            var roles = await _roleRepository.GetAllAsync();
            return roles.ToList() ?? new List<Role>();

        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            return await _userRepo.GetAllUsersWithRolesAsync();
        }

        public async Task<User> GetUserAndRolesAsync(long userId)
        {
            return await _userRepo.GetUsersWithRolesAsync(userId);
        }

        public async Task UpdateUser(Admin_AddUserDto model)
        {
            var user = await _userRepo.GetByIdAsync(model.Id);
            if (user == null)
            {
                throw new Exception("user Not Found");
            }

            // Cập nhật thông tin user
            user.FullName = model.Fullname;
            user.Email = model.Email;
            user.Phone = model.Phone;
            user.Gender = model.Gender;
            user.Dob = model.Dob;
            if (!string.IsNullOrEmpty(model.Password)) 
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            }

            // Xử lý avatar
            if (model.Avatar != null) // Nếu có file mới
            {
                user.AvatarUrl = await _fileUploadService.UploadFileAsync(model.Avatar);
            }
            // Nếu không chọn file mới, giữ nguyên AvatarUrl cũ từ user

            // Cập nhật Role
            var existingRole = user.UserRoles.FirstOrDefault();
            if (existingRole != null && existingRole.RoleId != model.RoleId)
            {
                user.UserRoles.Clear();
                user.UserRoles.Add(new UserRole { UserId = user.UserId, RoleId = model.RoleId });
            }

            await _userRepo.UpdateAsync(user);
        }
    }
}
