using System;
using UltraCryptoFolio.Models.Enums;

namespace UltraCryptoFolio.Repositories.DataAccessObjects
{
    public class PortfolioUserDao
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public UserRole Role { get; set; }
        public UserState State { get; set; }
    }
}
