using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Shared.Dto;
using VDMJasminka.Shared.Dto.Inventory;

namespace VDMJasminka.Shared.ViewModels
{
    public class CreateCheckupViewModel
    {
        public List<string> Diagnoses { get; set; }
        public OwnerViewModel Owner { get; set; }
        public PetViewModel Pet { get; set; }
        public List<MedicamentCheckupDto> Medicaments { get; set; }
    }
}