using MediatR;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.Application.Ambulance.Queries
{
    public class GetCheckupReportQuery : IRequest<CheckupReportViewModel>
    {
        public int OwnerId { get; }
        public int PetId { get; }
        public int CheckupId { get; }

        public GetCheckupReportQuery(int ownerId, int petId, int checkupId)
        {
            OwnerId = ownerId;
            PetId = petId;
            CheckupId = checkupId;
        }
    }

    public class CheckupReportViewModel
    {
        public CreateOwnerDto Owner { get; set; }
        public CreatePetDto Pet { get; set; }
        public CheckupDto Checkup { get; set; }
    }
}