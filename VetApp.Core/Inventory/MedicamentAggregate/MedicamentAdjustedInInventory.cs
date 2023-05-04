using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Inventory.MedicamentAggregate
{
    public class MedicamentAdjustedInInventory : IDomainEvent
    {
        public MedicamentAdjustedInInventory(Medicament item, InventoryAdjustment adjustment)
        {
            Item = item;
            Adjustment = adjustment;
        }

        public Medicament Item { get; }
        public InventoryAdjustment Adjustment { get; }
    }
}