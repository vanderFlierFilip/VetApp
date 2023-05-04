using MediatR;
using VDMJasminka.Shared.Dto.Ambulance;
using VDMJasminka.Shared.ViewModels;

namespace VDMJasminka.Application.Ambulance.Queries
{
    public class GetSingleOwnerByIdQuery : IRequest<OwnerListViewModel>
    {
        public GetSingleOwnerByIdQuery(int ownerId)
        {
            OwnerId = ownerId;
        }

        public int OwnerId { get; }
    }
}