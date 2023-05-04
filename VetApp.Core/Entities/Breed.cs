using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VDMJasminka.Core.Entities
{
    public partial class Breed
    {
        public int? Id { get; set; }
        public int AnimalTypeId { get; set; }
        public string Name { get; set; }
        public virtual Species Species { get; set; }

        public override string ToString()
        {
            return $"{Name}";
        }
    }
}
