using System.Collections.Generic;

namespace VDMJasminka.WebClient.Models
{
    public class OwnerAndPetTypeaheadViewModel
    {
        public int OwnerId { get; set; }
        public int PetId { get; set; }
        public IEnumerable<OwnerModel> OwnerModelList { get; set; }
    }
}
