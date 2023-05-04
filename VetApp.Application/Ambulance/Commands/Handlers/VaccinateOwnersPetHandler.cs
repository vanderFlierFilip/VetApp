using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Exceptions;
using VDMJasminka.Application.Extensions.Mapping;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Ambulance.Services;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Inventory.MedicamentAggregate;

namespace VDMJasminka.Application.Ambulance.Commands.Handlers
{
    public class VaccinateOwnersPetHandler : IRequestHandler<VaccinateOwnersPet>
    {
        private readonly IRepository<Owner> _ownerRepository;
        private readonly IVaccinationDateService _vaccinationDateService;
        private readonly ICheckupItemInventoryService _checkupItemInventoryService;

        public VaccinateOwnersPetHandler(IRepository<Owner> ownerRepository, IVaccinationDateService vaccinationDateService, ICheckupItemInventoryService checkupItemInventoryService)
        {
            _ownerRepository = ownerRepository;
            _vaccinationDateService = vaccinationDateService;
            _checkupItemInventoryService = checkupItemInventoryService;
        }

        public async Task<Unit> Handle(VaccinateOwnersPet request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.Get(request.OwnerId);

            if (owner == null)
            {
                throw new OwnerNotFoundException();
            }

            var date = DateTime.Now;
            var checkupItems = request.CheckupItems.ToEntityEnumerable();

            await owner.VaccinatePet(request.PetId,
                               date,
                               request.TypeOfVaccine,
                               (double)request.PriceOfCheckup,
                               checkupItems.ToList(),
                               request.NextVaccinationDate,
                               _vaccinationDateService, _checkupItemInventoryService);

            await _ownerRepository.Update(owner);

            return await Unit.Task;
        }
    }
}