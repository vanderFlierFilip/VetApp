using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VDMJasminka.Shared.Dto.Ambulance
{
    public class CreateOwnerDto
    {
        [Required]
        public string FullName { get; set; }

        public string Address { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Municipality { get; set; }

        [StringLength(13)]
        [MinLength(13)]
        [RegularExpression("^[0-9]*$")]
        public string? SSN { get; set; }

        public string AdditionalInfo { get; set; }

        [EmailAddress()]
        public string Email { get; set; }

        public List<CreatePetDto> Pets { get; set; }
    }
}