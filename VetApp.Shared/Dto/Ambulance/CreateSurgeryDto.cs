using System;
using System.Collections.Generic;
using System.Text;

namespace VDMJasminka.Shared.Dto.Ambulance
{
    public class CreateSurgeryDto : BaseCreateCheckupDto
    {
        public string Diagnosis { get; set; }
        public string Anamnesis { get; set; }
        public string ClinicalPicture { get; set; }
        public string Therapy { get; set; }
        public string Advice { get; set; }
        public string LabAnalysis { get; set; }
        public string SurgicalIntervention { get; set; }
    }
}
