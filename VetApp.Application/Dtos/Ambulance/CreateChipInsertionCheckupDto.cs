using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VDMJasminka.Application.Dtos.Ambulance
{
    public class CreateChipInsertionDto
    {
        [Required]
        [StringLength(15)]
        public string ChipNumber { get; set; }
        public CreateCheckupDto Checkup { get; set; }
    }
}
