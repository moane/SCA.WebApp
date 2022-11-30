using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SCA.WebApp.Models
{
    public class AssetViewModel
    {
        public AssetViewModel()
        {

            AcquisitionDate = DateTime.Now;
            OperationDate = DateTime.Now;
        }
        [Display(Name = "Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório")]
        [MinLength(3)]
        [MaxLength(100)]
        [Display(Name = "Nome")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "A data de aquisição é obrigatória")]
        [Display(Name = "Data Aquisição")]
        [DisplayFormat( DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime AcquisitionDate { get; set; }

        [Required(ErrorMessage = "A data início de operação é obrigatória")]
        [Display(Name = "Data Início Operação")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime OperationDate { get; set; }

        [Display(Name = "Necessita manutenção")]
        [DefaultValue(true)]
        public bool NeedMaintenance { get; set; }
        
        public bool Status { get; set; }

        [Display(Name = "Tipo de ativo")]
        public string? AssetTypeName { get; set; }

        [Display(Name = "Tipo de ativo")]
        [Required(ErrorMessage = "Selecione o tipo do ativo")]
        public int AssetTypeId { get; set; }
    }
}