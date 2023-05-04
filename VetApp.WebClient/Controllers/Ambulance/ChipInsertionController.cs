using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Commands;
using VDMJasminka.Application.Ambulance.Queries;
using VDMJasminka.WebClient.Models;

namespace VDMJasminka.WebClient.Controllers.Ambulance
{
    public class ChipInsertionController : Controller
    {
        private readonly IMediator _mediator;
        public ChipInsertionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> ChipPetAsync(int ownerId, int petId)
        {
            var petVM = await _mediator.Send(new GetPetWithOwnerQuery(ownerId, petId));
            CheckupDetailViewModel partialVm = await GetPartialVM();
            var diagnoses = await _mediator.Send(new GetAllDiagnosesQuery());
            var vm = new ChipInsertionCheckupViewModel()
            {
                Pet = petVM.Pet,
                Owner = petVM.Owner,
                OwnerId = ownerId,
                PetId = petId,
                CheckupDetailViewModel = await GetPartialVM()
            };
            return View(vm);
        }
        public async Task<IActionResult> SaveChipInsertion(ChipInsertionCheckupViewModel vm, CheckupDetailViewModel dvm)
        {
            var detailsList = dvm.CheckupDetailViewModels
              .Where(c => c != null)
              .Select(x => x.CheckupDetails).ToList();

            var paidSum = vm.PaidSum ?? 0;
            await _mediator.Send(new PerformChipInsertionCheckupOnPet(vm.OwnerId, vm.PetId, System.DateTime.Now, vm.ChipNumber, paidSum, detailsList));
            var checkupId = await _mediator.Send(new GetLatestCheckupForOwnerAndPetQuery(vm.OwnerId, vm.PetId));
            return RedirectToAction("Report", "Reports", new { checkupId, vm.OwnerId, vm.PetId });
        }
        public async Task<CheckupDetailViewModel> GetPartialVM()
        {
            var medApplications = new List<string> { "", "IM", "IV", "SC", "PO" };
            var medicaments = await _mediator.Send(new GetAllMedicamentsQuery());
            var partialVm = new CheckupDetailViewModel()
            {
                MedApplications = medApplications.Select(m => new SelectListItem { Text = m, Value = m }).ToList(),
                Medicaments = medicaments.Select(m => new SelectListItem { Text = m.Name, Value = m.Id.ToString() }).ToList(),
            };
            return partialVm;
        }

    }
}
