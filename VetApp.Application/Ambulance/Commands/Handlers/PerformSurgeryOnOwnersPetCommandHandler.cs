using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Exceptions;
using VDMJasminka.Application.Common;
using VDMJasminka.Application.Extensions.Mapping;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Ambulance.Services;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Inventory.MedicamentAggregate;

namespace VDMJasminka.Application.Ambulance.Commands.Handlers
{
    internal sealed class PerformSurgeryOnOwnersPetCommandHandler : IRequestHandler<PerformSurgeryOnOwnersPet>
    {
        private readonly IRepository<Owner> _ownerRepository;
        private readonly IDiagnosisService _diagnosisService;
        private readonly ICheckupItemInventoryService _checkupItemInventoryService;

        public PerformSurgeryOnOwnersPetCommandHandler(IRepository<Owner> ownerRepository, IDiagnosisService diagnosisService, ICheckupItemInventoryService checkupItemInventoryService)
        {
            _ownerRepository = ownerRepository;
            _diagnosisService = diagnosisService;
            _checkupItemInventoryService = checkupItemInventoryService;
        }

        public async Task<Unit> Handle(PerformSurgeryOnOwnersPet request, CancellationToken cancellationToken)
        {
            var owner = await _ownerRepository.Get(request.OwnerId);

            if (owner == null)
            {
                throw new OwnerNotFoundException();
            }

            var checkupItems = request.CheckupItems.ToEntityEnumerable();
            var today = DateTime.Now;

            owner.PerformSurgeryOnPet(request.PetId,
                                      today,
                                      request.Anamnesis,
                                      request.ClinicalPicture,
                                      request.Diagnosis,
                                      request.SurgicalIntervention,
                                      request.LabAnalysis,
                                      request.Reccommendation,
                                      request.ControlCheckupDate,
                                      checkupItems.ToList(),
                                      (double)request.CheckupPrice,
                                      request.RecommendedTherapy,
                                      _checkupItemInventoryService);

            await _diagnosisService.CheckIfDiagnosisExistsOrPersistAsync(request.Diagnosis);
            await _ownerRepository.Update(owner);

            return await Unit.Task;
        }
    }
}