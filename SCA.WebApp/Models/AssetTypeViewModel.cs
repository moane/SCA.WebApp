using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SCA.WebApp.Models
{
    public class AssetTypeViewModel
    {
        [Display(Name = "C�digo")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome � obrigat�rio")]
        [MinLength(3)]
        [MaxLength(100)]
        [Display(Name = "Nome")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "O intervalo de manuten��o em dias � obrigat�rio")]       
    
        [Display(Name = "Intervalo (dias)")]
        public int NumberMaintenanceDays { get; set; }

        [Display(Name = "Necessita manuten��o?")]
        public bool NeedMaintenance { get; set; }

        [ReadOnly(true)]
        public bool Status { get; set; }

    }
}