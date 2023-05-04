using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VDMJasminka.Shared.Dto.Ambulance
{
    public class CreateCheckupDto : BaseCreateCheckupDto
    {
        public string Diagnosis { get; set; }
        public string Anamnesis { get; set; }
        public string ClinicalPicture { get; set; }
        public string Therapy { get; set; }
        public string Advice { get; set; }
        public string LabAnalysis { get; set; }

    }


}
