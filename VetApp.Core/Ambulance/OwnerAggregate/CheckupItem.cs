using VDMJasminka.Core.Common;
using VDMJasminka.Core.Inventory.MedicamentAggregate;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace VDMJasminka.Core.Ambulance.OwnerAggregate
{
    public class CheckupItem : Entity
    {
        private CheckupItem()
        {
        }

        public CheckupItem(int medicamentId, double? amount, string uom, string medicamentApplication, double? price)
        {
            MedicamentId = medicamentId;
            Amount = amount;
            Uom = uom;
            MedicamentApplication = medicamentApplication;
            Price = price;
        }

        public override int Id { get; protected set; }
        public int? CheckupId { get; private set; }
        public int MedicamentId { get; private set; }
        public double? Amount { get; private set; }
        public string Uom { get; private set; }
        public string MedicamentApplication { get; private set; }
        public double? Price { get; private set; }
        public virtual Medicament Medicament { get; set; }

        public virtual Checkup Checkup { get; set; }

        public void SetAmount(double amount)
        {
            Amount = amount;
        }

        public void IncreaseAmount(double amount)
        {
            Amount += amount;
        }
    }
}