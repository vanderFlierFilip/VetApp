using System.Collections.Generic;
using VDMJasminka.Application.Dtos.Ambulance;

namespace VDMJasminka.WebClient.Models
{
    public class HomeViewModel
    {
        public IEnumerable<PendingVaccination> VaccinationsToday { get; set; }
        public IEnumerable<PendingVaccination> VaccinationsNextDaysInRange { get; set; }
        public IEnumerable<PetSearchViewModel> PetSearchList { get; set; }

    }
}