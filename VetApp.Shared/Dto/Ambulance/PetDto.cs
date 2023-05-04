using System;

namespace VDMJasminka.Shared.Dto.Ambulance
{
    public class PetDto
    {
        public int Id { get; set; }
        public string ChipNumber { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public SpeciesDto Species { get; set; }
        public BreedDto AnimalBreed { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Sex { get; set; }
        public DateTime? LastCheckup { get; set; }
        public string Description { get; set; }
        public string Alergy { get; set; }
        public string AccurateAge { get; set; }
    }
}
