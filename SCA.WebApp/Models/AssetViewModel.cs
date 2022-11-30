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
        [Display(Name = "C�digo")]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome � obrigat�rio")]
        [MinLength(3)]
        [MaxLength(100)]
        [Display(Name = "Nome")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "A data de aquisi��o � obrigat�ria")]
        [Display(Name = "Data Aquisi��o")]
        [DisplayFormat( DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inv�lido")]
        public DateTime AcquisitionDate { get; set; }

        [Required(ErrorMessage = "A data in�cio de opera��o � obrigat�ria")]
        [Display(Name = "Data In�cio Opera��o")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inv�lido")]
        public DateTime OperationDate { get; set; }

        [Display(Name = "Necessita manuten��o")]
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