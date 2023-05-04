using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace VDMJasminka.WebClient.Models
{
    public class SurgeryViewModel : BaseCheckupViewModel
    {
        public List<SelectListItem> Diagnoses { get; set; }
        public string Diagnosis { get; set; }
        public string Anamnesis { get; set; }
        public string ClinicalPicture { get; set; }
        public string Therapy { get; set; }
        public string SurgicalIntervention { get; set; }
        public string Advice { get; set; }
        public string LabAnalysis { get; set; }
        public DateTime? NextControlCheckup { get; set; }

    }
}
