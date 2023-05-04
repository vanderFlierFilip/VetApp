using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VDMJasminka.Application.Dtos.Ambulance
{
    public class CreateOrUpdateOwnerDto
    {
        public int? Id { get; set; }
        [Required]
        [StringLength(256)]
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Municipality { get; set; }
        public string SSN { get; set; }
        public string AdditionalInfo { get; set; }
        public IEnumerable<CreatePetDto> Pets { get; set; }
    }
}