using System;
using System.Collections.Generic;

namespace VDMJasminka.Shared.Dto.Ambulance
{
    public class ReadOwnerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Municipality { get; set; }
        public string SSN { get; set; }
        public decimal State { get; set; }
        public string AdditionalInfo { get; set; }
        public string Email { get; set; }
        public IEnumerable<ReadPetDto> Pets { get; set; }
    }

    public class ReadPetDto
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
}