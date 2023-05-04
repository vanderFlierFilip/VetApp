using System.ComponentModel.DataAnnotations.Schema;

namespace VDMJasminka.Core.Inventory.ValueObjects
{
    public class SupplierAddress
    {
        private SupplierAddress() { }
        public SupplierAddress(string address, string location, string phoneNumber)
        {
            Address = address;
            Location = location;
            PhoneNumber = phoneNumber;
        }

        public string Address { get; }
        public string Location { get; }
        public string PhoneNumber { get; }

    }
}
