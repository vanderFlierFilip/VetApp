namespace VDMJasminka.Shared.ViewModels
{
    public class MedicamentInventoryViewModel
    {
        public int MedicamentId { get; set; }
        public string MedicamentName { get; set; }
        public int? SupplierId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierPhoneNumber { get; set; }
        public string SupplierLocation { get; set; }
        public float? MinimalAmount { get; set; }
        public bool Material { get; set; }
        public double CurrentAmount { get; set; }
        public string Uom { get; set; }
        public double Price { get; set; }
    }
}