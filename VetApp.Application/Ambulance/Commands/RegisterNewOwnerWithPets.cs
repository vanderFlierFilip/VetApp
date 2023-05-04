using MediatR;
using System.Collections.Generic;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.Application.Ambulance.Commands
{
    public class RegisterNewOwnerWithPets : IRequest<ReadOwnerDto>
    {
        public RegisterNewOwnerWithPets(string fullName, string address, string location, string phoneNumber, string municipality, string sSN, string additionalInfo, string email, List<CreatePetDto> pets)
        {
            FullName = fullName;
            Address = address;
            Location = location;
            PhoneNumber = phoneNumber;
            Municipality = municipality;
            SSN = sSN;
            AdditionalInfo = additionalInfo;
            Email = email;
            Pets = pets;
        }

        public string FullName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Municipality { get; set; }
        public string SSN { get; set; }
        public string AdditionalInfo { get; set; }
        public string Email { get; set; }
        public List<CreatePetDto> Pets { get; }
    }
}