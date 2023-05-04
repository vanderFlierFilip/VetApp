namespace VDMJasminka.Core.Entities
{
    public class MedicamentInventory
    {
        public int MedicamentId { get; set; }
        public int? SupplierId { get; set; }
        public float? MinimalAmount { get; set; }
        public double CurrentAmount { get; set; }
        public string? Uom { get; set; }
    }
}