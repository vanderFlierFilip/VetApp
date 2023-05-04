using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Entities;

namespace VDMJasminka.Application.Dtos
{
    public class CreateCheckupDto
    {
        public int PetId { get; set; }
        public string Anamnesis { get; set; }
        public string ClinicalPicture { get; set; }
        public string Diagnosis { get; set; }
        public string Therapy { get; set; }
        public string SurgicalIntervention { get; set; }
        public string Advice { get; set; }
        public double? PaidSum { get; set; }
        public string Vaccine { get; set; }
        public string LabAnalysis { get; set; }
        public DateTime? NextControlCheckup { get; set; }
        public string CheckupType { get; set; }
        public IEnumerable<CheckupDetailsDto> CheckupDetails { get; set; }


    }
}
