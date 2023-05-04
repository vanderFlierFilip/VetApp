using System;

namespace VDMJasminka.Application.Dtos.Ambulance
{
    public class ReadPetDto
    {
        public int Id { get; set; }
        public string ChipNumber { get; set; }
        public string Name { get; set; }
        public ReadAnimalTypeDto AnimalType { get; set; }
        public ReadAnimalBreedDto AnimalBreed { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Sex { get; set; }
        public DateTime? LastCheckup { get; set; }
        public string Description { get; set; }
        public string Alergy { get; set; }
        public string AccurateAge { get; set; }

    }
}
