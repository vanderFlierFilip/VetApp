using System.ComponentModel.DataAnnotations;

namespace VDMJasminka.Shared.Dto.Inventory
{
    public class MedicamentDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Uom { get; set; }
        [Required]
        public double Price { get; set; }
        public short? MinimalAmount { get; set; }
        public bool InStock { get; set; }
        public bool? IsMaterial { get; set; }
    }
}
