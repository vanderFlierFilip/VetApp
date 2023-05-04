using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Shared.Dto;

namespace VDMJasminka.Shared.ViewModels
{
    public class CreateVaccinationViewModel
    {
        public List<dynamic> VaccineTypes { get => new List<dynamic> { new { Name = "Против заразни болести", Id = 0 }, new { Name = "Против беснило", Id = 1 } }; }
        public OwnerViewModel Owner { get; set; }
        public PetViewModel Pet { get; set; }
    }
}
