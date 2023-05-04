// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using System.Diagnostics;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Inventory.SupplierAggregate
{
    public class SupplierOrderItem
    {
        public SupplierOrderItem(int medicamentId, decimal price, decimal amount, string additionalInfo)
        {
            MedicamentId = medicamentId;
            Price = price;
            Amount = amount;
            AdditionalInfo = additionalInfo;
        }

        public SupplierOrderItem(int supplierOrderId, int medicamentId, decimal price, decimal amount, string additionalInfo)
        {
            SupplierOrderId = supplierOrderId;
            MedicamentId = medicamentId;
            Price = price;
            Amount = amount;
            AdditionalInfo = additionalInfo;
        }

        public int SupplierOrderId { get; set; }
        public int MedicamentId { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public string AdditionalInfo { get; set; }

        public void IncreaseAmount(decimal amount)
        {
            Amount += amount;
        }
    }
}