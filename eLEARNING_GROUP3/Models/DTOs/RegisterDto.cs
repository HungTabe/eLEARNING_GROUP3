using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.DTOs
{
	public class RegisterDto
	{
        [Required(ErrorMessage = "FullName không được để trống!")]
        [MaxLength(20, ErrorMessage = "FullName không được vượt quá 20 ký tự!")]
        public string FullName { get; set; }


        [Required(ErrorMessage = "Email không được để trống!")]
		[EmailAddress(ErrorMessage = "Email không hợp lệ!")]
		public string Email { get; set; }

		[Required(ErrorMessage = "Mật khẩu không được để trống!")]
		[MinLength(6, ErrorMessage = "Mật khẩu phải có ít nhất 6 ký tự!")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{8,}$",
        ErrorMessage = "Password must have (A-Z), (a-z), (0-9), a special char, and be 8+ chars.")]
        public string Password { get; set; }


		[Required(ErrorMessage = "Vui lòng nhập lại mật khẩu!")]
		[Compare("Password", ErrorMessage = "Mật khẩu không khớp!")]
		public string ConfirmPassword { get; set; }
	}

}
