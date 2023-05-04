namespace VDMJasminka.Shared.Dto.Inventory
{
    public class MedicamentCheckupDto
    {
        public int MedicamentId { get; set; }
        public string MedicamentName { get; set; }
        public string Uom { get; set; }
        public double Price { get; set; }
        public double CurrentAmount { get; set; }
    }
}