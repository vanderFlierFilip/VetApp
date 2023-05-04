using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Application.Dtos.Ambulance;

namespace VDMJasminka.Application.Dtos
{
    public class CheckupDto
    {
        public int? Id { get; set; }
        public string Anamnesis { get; set; }
        public string ClinicalPicture { get; set; }
        public string Diagnosis { get; set; }
        public string Therapy { get; set; }
        public string SurgicalIntervention { get; set; }
        public string Advice { get; set; }
        public double? PaidSum { get; set; }
        public DateTime? NextVaccinationDate { get; set; }
        public string Vaccine { get; set; }
        public string LabAnalysis { get; set; }
        public DateTime? NextControlCheckup { get; set; }
        public string CheckupType { get; set; }
        public ReadPetDto Pet{ get; set; }
        public IEnumerable<CheckupDetailsDto> CheckupDetails { get; set; }

    }
}
