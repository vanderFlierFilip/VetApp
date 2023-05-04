namespace VDMJasminka.Shared.Dto.Inventory
{
    public class SupplierDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class CreateSupplierDto
    {
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
    }
}