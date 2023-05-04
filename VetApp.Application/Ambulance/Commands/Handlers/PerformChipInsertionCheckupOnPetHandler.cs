using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Exceptions;
using VDMJasminka.Application.Extensions.Mapping;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Ambulance.Services;
using VDMJasminka.Core.Interfaces;

namespace VDMJasminka.Application.Ambulance.Commands.Handlers
{
    internal sealed class PerformChipInsertionCheckupOnPetHandler : IRequestHandler<PerformChipInsertionCheckupOnPet>
    {
        private readonly IRepository<Owner> _repository;
        private readonly ICheckupItemInventoryService _checkupItemInventoryService;

        public PerformChipInsertionCheckupOnPetHandler(IRepository<Owner> repository, ICheckupItemInventoryService checkupItemInventoryService)
        {
            _repository = repository;
            _checkupItemInventoryService = checkupItemInventoryService;
        }

        public async Task<Unit> Handle(PerformChipInsertionCheckupOnPet request, CancellationToken cancellationToken)
        {
            var owner = await _repository.Get(request.OwnerId);

            if (owner == null)
            {
                throw new OwnerNotFoundException();
            }

            var checkupItems = request.CheckupItems.ToEntityEnumerable();

            owner.ChipPet(request.PetId, request.DateOfChipping, request.ChipNumber, (double)request.PaidSum, checkupItems.ToList(), _checkupItemInventoryService);

            await _repository.Update(owner);

            return await Unit.Task;
        }
    }
}