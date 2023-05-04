using MediatR;

namespace VDMJasminka.Application.Ambulance.Commands
{
    public class UpdateOwnerLivingAddress : IRequest
    {
        public UpdateOwnerLivingAddress(int ownerId, string address, string location, string municipality)
        {
            OwnerId = ownerId;
            Address = address;
            Location = location;
            Municipality = municipality;
        }
        public int OwnerId { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string Municipality { get; set; }
    }
}
