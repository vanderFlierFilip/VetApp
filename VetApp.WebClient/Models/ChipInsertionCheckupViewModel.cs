using System.ComponentModel.DataAnnotations;

namespace VDMJasminka.WebClient.Models
{
    public class ChipInsertionCheckupViewModel : BaseCheckupViewModel
    {
        [MaxLength(15, ErrorMessage = "Бројот на чипот мора да содржи 15 бројки")]
        [Required(ErrorMessage = "Полето е задолжително")]
        [MinLength(15, ErrorMessage = "Бројот на чипот мора да содржи 15 бројки")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Бројот на чипот не смее да содржи алфабетски карактери")]
        public string ChipNumber { get; set; }
    }
}
