using System;
using System.ComponentModel.DataAnnotations;

namespace VDMJasminka.Shared.Dto.Ambulance
{
    public class CreatePetDto
    {
        public string ChipNumber { get; set; }

        [Required]
        public string Name { get; set; }

        public int SpeciesId { get; set; }
        public int BreedId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string Description { get; set; }
        public string Alergy { get; set; }
    }
}