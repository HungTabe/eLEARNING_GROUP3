using System.ComponentModel.DataAnnotations;
namespace OnlineLearning.Models.DTOs
{
	public class ChangePassDTO
	{
		[Required(ErrorMessage = "Current password is required.")]
		[DataType(DataType.Password)]
		public string CurrentPassword { get; set; }

		[Required(ErrorMessage = "New password is required.")]
		[DataType(DataType.Password)]
		[MinLength(6, ErrorMessage = "New password must be at least 6 characters long.")]
		[MaxLength(100, ErrorMessage = "New password cannot exceed 100 characters.")]
		[RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$",
			ErrorMessage = "New password must have at least one uppercase letter, one lowercase letter, and one number.")]
		public string NewPassword { get; set; }

		[Required(ErrorMessage = "Confirm password is required.")]
		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "Confirm password does not match new password.")]
		public string ConfirmPassword { get; set; }
	}
}
