using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VDMJasminka.WebClient.Models
{
    public class OwnerModel
    {
        public int? Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Municipality { get; set; }
        public string SSN { get; set; }
        public string AdditionalInfo { get; set; }
        public IEnumerable<PetModel> Pets { get; set; }
        public string Email { get; set; }

    }
}
