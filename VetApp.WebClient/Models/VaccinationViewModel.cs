using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.WebClient.Models
{
    public class VaccinationViewModel : BaseCheckupViewModel
    {
        public string Vaccine { get; set; }
        public List<SelectListItem> Vaccines { get; set; }
        public DateTime? NextVaccinationDate { get; set; }
    }
    public class CheckupDetailViewModel
    {
        public CheckupDetailViewModel()
        {
            CheckupDetailViewModels = new List<CheckupDetailViewModel>();
        }
        public List<SelectListItem> Medicaments { get; set; }
        public List<SelectListItem> MedApplications { get; set; }
        public CheckupDetailsDto CheckupDetails { get; set; }
        public List<CheckupDetailViewModel> CheckupDetailViewModels { get; set; }

    }
}

