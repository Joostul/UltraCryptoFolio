using Microsoft.AspNetCore.Identity;
using UltraCryptoFolio.Models.Enums;

namespace UltraCryptoFolio.Models.DomainModels
{
    public class PortfolioUser
    {
        private string _password { get; set; }
        public PortfolioUser(string userName, string password, bool hashPassword)
        {
            UserEmail = userName;

            if (hashPassword)
            {
                var pwhasher = new PasswordHasher<string>();
                _password = pwhasher.HashPassword(UserEmail, password);
            }
            else
            {
                _password = password;
            }
        }

        public string UserEmail { get; set; }
        public string Password { get { return _password; } }
        public UserRole Role { get; set; }
        public UserState State { get; set; }

        public bool ValidatePassword(string password)
        {
            var pwhasher = new PasswordHasher<string>();
            //var hashedPassword = pwhasher.HashPassword(UserEmail, password);
            //return string.Compare(hashedPassword, _password) == 0;
            return pwhasher.VerifyHashedPassword(UserEmail, _password, password) == PasswordVerificationResult.Success;
        }
    }
}
