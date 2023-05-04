using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.Shared.ViewModels
{
    public class PetDetailsViewModel
    {
        public PetViewModel Pet { get; set; }
        public OwnerViewModel Owner { get; set; }
    }

    public class OtherPetsModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PetViewModel
    {
        public int Id { get; set; }
        public string ChipNumber { get; set; }
        public int OwnerId { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Sex { get; set; }
        public DateTime? LastCheckup { get; set; }
        public string Description { get; set; }
        public string Alergy { get; set; }
        public string AccurateAge { get; set; }
    }

    public class OwnerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Municipality { get; set; }
        public string SSN { get; set; }
        public decimal State { get; set; }
        public string AdditionalInfo { get; set; }
        public string Email { get; set; }
        public List<OtherPetsModel> Pets { get; set; } = new List<OtherPetsModel>();
    }
}