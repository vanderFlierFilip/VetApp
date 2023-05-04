// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Inventory.SupplierAggregate
{
    public class SupplierOrder : Entity
    {
        private SupplierOrder()
        {
        }

        public SupplierOrder(int supplierId, DateTime date, string description)
        {
            SupplierId = supplierId;
            Date = date;
            InvoiceNumber = description;
        }

        public override int Id { get; protected set; }
        public int SupplierId { get; }
        public DateTime Date { get; }
        public string InvoiceNumber { get; }
        public IReadOnlyCollection<SupplierOrderItem> Items => _inventoryItems.AsReadOnly();

        private List<SupplierOrderItem> _inventoryItems = new List<SupplierOrderItem>();

        public void AddItem(int medicamentId, decimal price, decimal amount, string additionalInfo)
        {
            if (!_inventoryItems.Any(i => i.MedicamentId == medicamentId))
            {
                var item = new SupplierOrderItem(Id, medicamentId, price, amount, additionalInfo);
                _inventoryItems.Add(item);
                return;
            }

            var existing = _inventoryItems.FirstOrDefault(i => i.MedicamentId == medicamentId);
            existing.IncreaseAmount(amount);
        }
    }
}