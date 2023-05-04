using VDMJasminka.Core.Ambulance.OwnerAggregate;

namespace VDMJasminka.Core.Ambulance.Events
{
    public class OwnerCheckupIsNotPaid
    {
        public Owner Owner { get; }
        public decimal SumNotPaid { get; }

        public OwnerCheckupIsNotPaid(Owner owner, decimal sumOfCheckupCost)
        {
            Owner = owner;
            SumNotPaid = sumOfCheckupCost;
        }
    }
}