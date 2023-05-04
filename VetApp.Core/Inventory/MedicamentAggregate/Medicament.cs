using System.Collections.Generic;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Common;
using VDMJasminka.Core.Inventory.Services;
using VDMJasminka.Core.Inventory.SupplierAggregate;
using VDMJasminka.Core.Inventory.ValueObjects;

namespace VDMJasminka.Core.Inventory.MedicamentAggregate
{
    public class Medicament : Entity, IAggregateRoot
    {
        private List<InventoryAdjustment> _inventoryAdjustments = new List<InventoryAdjustment>();
        private Medicament()
        {
           
        }

        public Medicament(string name, string uom, double price, int minimalAmount, bool isMaterial)
        {
            MedicamentDetails = new MedicamentDetails(name, uom, isMaterial);
            MinimalAmount = new MedicamentMinimalAmount(minimalAmount);
            Price = new MedicamentPrice(price);
        }

        public override int Id { get; protected set; }
        public MedicamentDetails MedicamentDetails { get; private set; }
        public MedicamentMinimalAmount MinimalAmount { get; private set; }
        public MedicamentPrice Price { get; private set; }
        public IReadOnlyList<InventoryAdjustment> InventoryAdjustments => _inventoryAdjustments.AsReadOnly();
        public bool ChangePrice(double amount)
        {
            var newPrice = new MedicamentPrice(amount);
            
            if (newPrice != Price)
            {
                Price = newPrice;
            }
            return newPrice != Price;
        }

        public void AdjustAmountInInventory(double amount, string reason)
        {
            var adjustment = new InventoryAdjustment(Id, amount, new InventoryAdjustmentReason(reason));
            
            _inventoryAdjustments.Add(adjustment);

            DomainEvents.Add(new MedicamentAdjustedInInventory(this, adjustment));
        }

        public void ChangeMinimalAmountInStock(int amount)
        {
            var newAmount = new MedicamentMinimalAmount(amount);

            if (newAmount != MinimalAmount)
            {
                MinimalAmount = newAmount;
            }

            return;
        }

        public bool IsInStock(IMedicamentInventoryService service)
        {
            return service.IsMedicamentInStock(Id);
        }
        
        public short GetMinimalAmount()
        {
            return (short)MinimalAmount.Value;
        }

        public void ChangeDetails(string name, string uom, bool isMaterial)
        {
            MedicamentDetails = new MedicamentDetails(name, uom, isMaterial);
        }

        
    }
}
