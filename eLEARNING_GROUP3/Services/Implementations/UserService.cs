using OnlineLearning.Enums;
using OnlineLearning.Models.Domains.UserModels;
using OnlineLearning.Models.DTOs;
using OnlineLearning.Repositories.Interfaces;
using OnlineLearning.Services.Interfaces;

namespace OnlineLearning.Services.Implementations
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IUserRoleRepository _userRoleRepository;
		public UserService(IUserRepository userRepository, IUserRoleRepository userRoleRepository)
		{
			_userRepository = userRepository;
			_userRoleRepository = userRoleRepository;
		}

		public async Task<User> AddUserAsync(User user)
		{
			return await _userRepository.AddAsync(user);
		}

		public async Task<IEnumerable<User>> GetAllUsersAsync()
		{
			return await _userRepository.GetAllAsync();
		}

		public async Task<User?> GetByEmailAndPasswordAsync(string email, string password)
		{
			return await _userRepository.GetByEmailAndPassword(email, password);
		}

		public async Task<User?> GetUserByEmailAsync(string email)
		{
			return await _userRepository.GetUserByEmail(email);
		}

		public async Task<User?> GetUserByIdAsync(long? id)
		{
			return await _userRepository.GetByIdAsync(id);
		}

		public async Task UpdateUserAsync(User user)
		{
			await _userRepository.UpdateAsync(user);
		}



		/*	UPDATE PROFILE	*/
		public async Task<bool> UpdateProfileAsync(ProfileDTO profile)
		{
			return await _userRepository.UpdateUserInfoAsync(profile);
		}

		/*	CHANGE AVATAR	*/
		public async Task<bool> ChangeAvatarAsync(long userId, IFormFile avatarFile)
		{
			// Check file rỗng or ko có
			if (avatarFile == null || avatarFile.Length == 0)
			{
				return false;
			}

			// Check size
			if (avatarFile.Length > 800 * 1024)
			{
				return false;
			}

			// Format file
			var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
			var fileExtension = Path.GetExtension(avatarFile.FileName);
			if (!allowedExtensions.Contains(fileExtension.ToLower()))
			{
				return false;
			}

			// Create random file name 
			string fileName = Guid.NewGuid() + fileExtension;
			string imagesFolderPath = Path.Combine("wwwroot", "uploads");

			// Check folder tồn tại ? dùng luôn : tạo mới
			if (!Directory.Exists(imagesFolderPath))
			{
				Directory.CreateDirectory(imagesFolderPath);
			}

			string filePath = Path.Combine(imagesFolderPath, fileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await avatarFile.CopyToAsync(stream);
			}

			// Update avt URL
			string newUrl = "/uploads/" + fileName;
			return await _userRepository.UpdateAvatarAsync(userId, newUrl);
		}
		public async Task<string> GetUserHeaderAsync(long? userId)
		{
			if (userId.HasValue)
			{
				var roles = await _userRoleRepository.GetRolesByUserIdAsync(userId);
				if (roles.Contains(RoleType.ADMIN))
					return "_AdminHeader";
				else if (roles.Contains(RoleType.MENTOR))
					return "_MentorHeader";
				else if (roles.Contains(RoleType.MENTEE))
					return "_MenteeHeader";
				else
					return "_GuestHeader";
			}
			return "_GuestHeader";
		}

		public async Task<bool> ChangePasswordAsync(long userId, ChangePassDTO changePassDTO)
		{
			var user = await _userRepository.GetByIdAsync(userId);
			if (user == null) { return false; }

			user.Password = BCrypt.Net.BCrypt.HashPassword(changePassDTO.NewPassword);
			_userRepository.UpdateAsync(user);
			return true;
		}
	}
}
