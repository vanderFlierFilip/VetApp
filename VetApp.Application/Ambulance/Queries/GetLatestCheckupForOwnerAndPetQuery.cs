using MediatR;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.Application.Ambulance.Queries
{
    public class GetLatestCheckupForOwnerAndPetQuery : IRequest<CheckupDto>
    {
        public int OwnerId { get; }
        public int PetId { get; }
        public GetLatestCheckupForOwnerAndPetQuery(int ownerId, int petId)
        {
            OwnerId = ownerId;
            PetId = petId;
        }
    }
}
