using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Entities;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VDMJasminka.Core
{
    public partial class Species
    {
        public Species()
        {
            Pets = new HashSet<Pet>();
            Breeds = new HashSet<Breed>();
        }
        public int? Id { get; set; }
        public string Type { get; set; }

        public override string ToString()
        {
            return $"{Type}";
        }
        public virtual ICollection<Pet> Pets { get; set; }
        public virtual ICollection<Breed> Breeds { get; set; }
    }
}
