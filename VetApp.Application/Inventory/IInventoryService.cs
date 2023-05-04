using System.Collections.Generic;
using System.Threading.Tasks;
using VDMJasminka.Application.Dtos.Inventory;
using VDMJasminka.Core.Inventory.MedicamentAggregate;
using VDMJasminka.Core.Inventory.SupplierAggregate;

namespace VDMJasminka.Application.Inventory
{
    public interface IInventoryService
    {
        IEnumerable<InventoryStateObject> GetCurrentInventoryState();
        Task MakeNewInventoryEntry(CreateInventoryEntryDto dto);
    }

    public class InventoryStateObject
    {
        public Medicament Medicament { get; set; }
        public decimal CurrentAmountInInventory { get; set; }
        public Supplier Supplier { get; set; }


    }
}