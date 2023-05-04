using System.Collections.Generic;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.Shared.Dtos
{
    public class PetDetailsViewModel
    {
        public PetDto Pet { get; set; }
        public CreateOwnerDto Owner { get; set; }
        public List<CheckupDto> Vaccinations { get; set; }
        public List<CheckupDto> Checkups { get; set; }
        public List<CheckupDto> ChipInsertionCheckups { get; set; }
        public List<CheckupDto> SurgicalInterventions { get; set; }
    }
}
