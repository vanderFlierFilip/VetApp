using System.Collections.Generic;
using VDMJasminka.Core;
using VDMJasminka.Core.Entities;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.WebClient.Models
{
    public class EditOwnerViewModel
    {
        public CreateOwnerDto Owner { get; set; }
        public PetModel Pet { get; set; }
        public IEnumerable<Species> AnimalTypes { get; set; }
        public ICollection<Breed> Breeds { get; set; }
        public IList<string> AnimalSexOptions = new List<string> { "M", "Ž" };
    }
}
