// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable
using System;
using System.Collections.Generic;
using VDMJasminka.Core.Common;
using VDMJasminka.Core.Inventory.Events;
using VDMJasminka.Core.Inventory.ValueObjects;

namespace VDMJasminka.Core.Inventory.SupplierAggregate
{
    public class Supplier : Entity, IAggregateRoot
    {
        public Supplier()
        {
        }

        public override int Id { get; protected set; }

        public Supplier(string name, string address, string location, string phoneNumber)
        {
            Name = name;
            Address = new SupplierAddress(address, location, phoneNumber);
        }

        public string Name { get; private set; }
        public SupplierAddress Address { get; private set; }

        private List<SupplierOrder> _orderRecievments = new List<SupplierOrder>();

        public void RecieveOrderDelivery(DateTime entryDate, string invoiceNo, List<SupplierOrderItem> items)
        {
            var order = new SupplierOrder(Id, entryDate, invoiceNo);

            foreach (var item in items)
            {
                order.AddItem(item.MedicamentId, item.Price, item.Amount, item.AdditionalInfo);
            }

            _orderRecievments.Add(order);

            AddDomainEvent(new InventoryEntryCreated(this));
        }

        public void ChangeAddress(string address, string location, string phoneNumber)
        {
            Address = new SupplierAddress(address, location, phoneNumber);
            AddDomainEvent(new SupplierAddressChanged(Address));
        }

        public IReadOnlyCollection<SupplierOrder> OrderRecievments => _orderRecievments.AsReadOnly();
    }
}