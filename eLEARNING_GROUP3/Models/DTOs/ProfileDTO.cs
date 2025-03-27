using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace OnlineLearning.Models.DTOs
{
	public class ProfileDTO
	{
		[Required]
		public long UserId { get; set; }

		[Required(ErrorMessage = "Name cannot be empty!")]
		[StringLength(100, ErrorMessage = "Name must be between 2 and 100 characters.", MinimumLength = 2)]
		[RegularExpression(@"^[a-zA-ZÀ-Ỹà-ỹ\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
		public string? FullName { get; set; }

		[StringLength(20, ErrorMessage = "Phone number cannot exceed 20 characters.")]
		[Required(ErrorMessage = "Phone number is required.")]
		[RegularExpression(@"^0\d{9}$", ErrorMessage = "Invalid phone number format!")]
		public string? Phone { get; set; }

		public string Email { get; set; }

		public string? AvatarUrl { get; set; }

		public bool? Gender { get; set; }

		[Required(ErrorMessage = "Date of Birth is required")]
		[DataType(DataType.Date)]
		[CustomValidation(typeof(ProfileDTO), nameof(ValidateDateOfBirth))]
		public DateOnly? Dob { get; set; }

		public static ValidationResult ValidateDateOfBirth(DateOnly? date, ValidationContext context)
		{
			if (date == null)
			{
				return new ValidationResult("Date of Birth is required.");
			}

			if (date > DateOnly.FromDateTime(DateTime.Today))
			{
				return new ValidationResult("Date of Birth cannot be in the future.");
			}

			return ValidationResult.Success;
		}

	}
}
