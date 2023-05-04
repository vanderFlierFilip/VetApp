using System;
using System.ComponentModel.DataAnnotations;

namespace VDMJasminka.Application.Dtos.Ambulance
{
    public class CreatePetDto
    {
        public int? Id { get; set; }
        public string ChipNumber { get; set; }
        [Required]
        public int OwnerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int AnimalTypeId { get; set; }
        [Required]
        public int BreedId { get; set; }
        [Required]
        public DateTime? DateOfBirth { get; set; }
        [Required]
        public string Sex { get; set; }
        public string Description { get; set; }
        public string Alergy { get; set; }
    }
}
