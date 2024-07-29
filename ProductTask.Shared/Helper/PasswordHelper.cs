using Microsoft.AspNetCore.Identity;

namespace ButterflyApi.SharedKernal.Helper
{
    public class PasswordHelper
	{
		public static string HashPassword(string password)
		{
			var passwordHasher = new PasswordHasher<string>();
			string hashedPassword = passwordHasher.HashPassword(null, password);
			return hashedPassword;
		}

		public static bool VerifyPassword(string password, string hashedPassword)
		{
			var passwordHasher = new PasswordHasher<string>();
			var passwordVerificationResult = passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
			return passwordVerificationResult == PasswordVerificationResult.Success;
		}
	}
}
