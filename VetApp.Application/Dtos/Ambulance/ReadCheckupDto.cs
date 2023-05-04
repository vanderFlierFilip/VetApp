using System;
using System.Collections.Generic;
using System.Text;

namespace VDMJasminka.Application.Dtos.Ambulance
{
    public class ReadCheckupDto
    {
        public int Id { get; set; }
        public string Anamnesis { get; set; }
        public string ClinicalPicture { get; set; }
        public string Diagnosis { get; set; }
        public string Therapy { get; set; }
        public string SurgicalIntervention { get; set; }
        public string Advice { get; set; }
        public double? PaidSum { get; set; }
        public string Vaccine { get; set; }
        public string CheckupType { get; set; }
        public ReadPetDto Pet { get; set; }
        public IEnumerable<CheckupDetailsDto> CheckupDetails { get; set; }
    }
}
