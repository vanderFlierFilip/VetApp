// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using VDMJasminka.Core.Common;
using VDMJasminka.Core.Inventory.ValueObjects;

namespace VDMJasminka.Core.Inventory.Events
{
    internal class SupplierAddressChanged : IDomainEvent
    {
        public SupplierAddressChanged(SupplierAddress address)
        {
            Address = address;
        }

        public SupplierAddress Address { get; }
    }
}