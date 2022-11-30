using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SCA.WebApp.Models
{
    public class AssetTypeViewModel
    {
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(3)]
        [MaxLength(100)]
        [Display(Name = "Nome")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "O intervalo de manutenção em dias é obrigatório")]       
    
        [Display(Name = "Intervalo (dias)")]
        public int NumberMaintenanceDays { get; set; }

        [Display(Name = "Necessita manutenção?")]
        public bool NeedMaintenance { get; set; }

        [ReadOnly(true)]
        public bool Status { get; set; }

    }
}