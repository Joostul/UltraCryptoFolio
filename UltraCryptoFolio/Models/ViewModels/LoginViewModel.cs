using System.ComponentModel.DataAnnotations;

namespace UltraCryptoFolio.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string UserEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PassWord { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}
