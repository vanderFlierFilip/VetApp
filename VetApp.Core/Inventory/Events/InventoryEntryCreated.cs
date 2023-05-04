using VDMJasminka.Core.Common;
using VDMJasminka.Core.Inventory.SupplierAggregate;

namespace VDMJasminka.Core.Inventory.Events
{
    public class InventoryEntryCreated : IDomainEvent
    {
        public Supplier Supplier { get; }

        public InventoryEntryCreated(Supplier supplier)
        {
            Supplier = supplier;
        }


    }
}
