using System.Collections.Generic;

namespace VDMJasminka.Shared.ViewModels
{
    public class OwnerListViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Municipality { get; set; }
        public string SSN { get; set; }
        public string AdditionalInfo { get; set; }
        public string Email { get; set; }
        public List<OwnerListPetViewModel> Pets { get; set; } = new List<OwnerListPetViewModel>();
    }

    public class OwnerListPetViewModel
    {
        public int PetId { get; set; }
        public string PetName { get; set; }
    }
}