
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SCA.WebApp.Models
{
    public class AccountViewModel
    {
        [Required]
        [DisplayName("Email")]
        public string? User { get; set; }

        [Required]
        [DisplayName("Senha")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [DisplayName("Lembrar-me")]
        public bool RememberMe { get; set; }
    }
}