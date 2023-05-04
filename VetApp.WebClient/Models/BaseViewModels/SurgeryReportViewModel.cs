using System;

namespace VDMJasminka.WebClient.Models.BaseViewModels
{
    public class SurgeryReportViewModel : PdfRecordViewModel
    {
        public string Diagnosis { get; set; }
        public string Anamnesis { get; set; }
        public string ClinicalPicture { get; set; }
        public string Therapy { get; set; }
        public string SurgicalIntervention { get; set; }
        public string Advice { get; set; }
        public string LabAnalysis { get; set; }
        public DateTime? NextControlCheckup { get; set; }
        public double? PaidSum { get; set; }
        public DateTime? NextVaccinationDate { get; set; }
    }
}
