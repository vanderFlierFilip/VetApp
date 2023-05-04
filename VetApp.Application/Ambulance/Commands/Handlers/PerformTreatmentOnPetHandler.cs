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
    internal sealed class PerformTreatmentOnPetHandler : IRequestHandler<PerformTreatmentOnPet>
    {
        private readonly IRepository<Owner> _repository;
        private readonly ICheckupItemInventoryService _checkupItemInventoryService;

        public PerformTreatmentOnPetHandler(IRepository<Owner> repository, ICheckupItemInventoryService checkupItemInventoryService)
        {
            _repository = repository;
            _checkupItemInventoryService = checkupItemInventoryService;
        }

        public async Task<Unit> Handle(PerformTreatmentOnPet request, CancellationToken cancellationToken)
        {
            var owner = await _repository.Get(request.OwnerId);

            if (owner == null)
            {
                throw new OwnerNotFoundException();
            }

            var checkupItems = request.CheckupItems.ToEntityEnumerable();

            owner.PerformTreatmentOnPet(petId: request.PetId,
                                        checkupDate: request.CheckupDate,
                                        anamnesis: request.Anamnesis,
                                        clinicalPicture: request.ClinicalPicture,
                                        diagnosis: request.Diagnosis,
                                        labAnalysis: request.LabAnalysis,
                                        reccommendation: request.Reccommendation,
                                        controlCheckupDate: request.ControlCheckupDate,
                                        checkupItems: checkupItems.ToList(),
                                        paidSum: (double)request.PaidSum,
                                        reccommendedTherapy: request.ReccommendedTherapy, inventoryService: _checkupItemInventoryService);

            await _repository.Update(owner);

            return await Unit.Task;
        }
    }
}