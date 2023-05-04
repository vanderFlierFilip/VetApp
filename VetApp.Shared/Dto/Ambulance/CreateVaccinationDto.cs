using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VDMJasminka.Shared.Dto.Ambulance
{
    public class CreateVaccinationDto : BaseCreateCheckupDto
    {
        [Required]
        public string Vaccine { get; set; }

        public DateTime? NextVaccinationDate { get; set; }
    }
}