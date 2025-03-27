﻿namespace OnlineLearning.Services.Interfaces
{
	public interface IEmailService
	{
		Task SendOtpEmailAsync(string toEmail, string otp);

		Task SendNewPasswordEmailAsync(string toEmail);
	}
}
