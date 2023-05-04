using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Ambulance.Events
{
    public class OwnerLivingAddressChanged : IDomainEvent
    {
        public OwnerLivingAddressChanged(string address, string location, string municipality)
        {
            Address = address;
            Location = location;
            Municipality = municipality;
        }

        public string Address { get; }
        public string Location { get; }
        public string Municipality { get; }
    }
}