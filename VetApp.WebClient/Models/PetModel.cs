using System;
using System.ComponentModel.DataAnnotations;
using VDMJasminka.Core;
using VDMJasminka.Core.Entities;

namespace VDMJasminka.WebClient.Models
{
    public class PetModel
    {

        public int? PetId { get; set; }
        public string ChipNumber { get; set; }
        [Required(ErrorMessage = "Полето име е задолжително")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Полето врста е задолжително")]
        public int SpeciesId { get; set; }
        [Required(ErrorMessage = "Полето раса е задолжително")]
        public int BreedId { get; set; }
        [Required(ErrorMessage = "Полето датум на раѓање е задолжително")]
        public DateTime? DateOfBirth { get; set; }
        [Required(ErrorMessage = "Полето пол е задолжително")]
        public string Sex { get; set; }
        public int OwnerId { get; set; }

        public DateTime? LastCheckup { get; set; }
        public string Description { get; set; }
        public string Alergy { get; set; }
        public Species Species { get; set; }
        public Breed Breed { get; set; }
    }
}
