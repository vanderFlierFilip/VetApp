using MediatR;

namespace VDMJasminka.Application.Ambulance.Queries
{
    public class GetLatestCheckupForPet : IRequest<int>
    {
        public GetLatestCheckupForPet(int ownerId, int petId, string category)
        {
            OwnerId = ownerId;
            PetId = petId;
            Category = category;
        }

        public int OwnerId { get; }
        public int PetId { get; }
        public string Category { get; }
    }
}
