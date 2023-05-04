using System;
using VDMJasminka.Core.Common;
using VDMJasminka.Core.Inventory.ValueObjects;

namespace VDMJasminka.Core.Inventory.MedicamentAggregate
{
    public class InventoryAdjustment : Entity
    {
        private InventoryAdjustment() { }

        public InventoryAdjustment(int medicamentId, double amount, InventoryAdjustmentReason reason)
        {
            MedicamentId = medicamentId;
            Amount = amount;
            Reason = reason;
            Date = DateTime.Now;
        }
        public DateTime Date{ get; }
        public int MedicamentId { get; }
        public double Amount { get; }
        public InventoryAdjustmentReason Reason { get; }
    }
}
