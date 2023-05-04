using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Commands;
using VDMJasminka.Application.Ambulance.Queries;
using VDMJasminka.Shared.Constants;
using VDMJasminka.WebClient.Models;

namespace VDMJasminka.WebClient.Controllers.Ambulance
{
    public class CheckupController : Controller
    {
        private readonly IMediator _mediator;

        public CheckupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Checkup(int ownerId, int petId)
        {
            var petVM = await _mediator.Send(new GetPetWithOwnerQuery(ownerId, petId));
            CheckupDetailViewModel partialVm = await GetPartialVM();
            var diagnoses = await _mediator.Send(new GetAllDiagnosesQuery());
            var vm = new CheckupViewModel()
            {
                Pet = petVM.Pet,
                Owner = petVM.Owner,
                OwnerId = ownerId,
                PetId = petId,
                CheckupDetailViewModel = partialVm,
                Diagnoses = diagnoses.Select(s => new SelectListItem { Text = s, Value = s }).ToList()
            };

            return View(vm);
        }



        public async Task<IActionResult> SaveCheckup(CheckupViewModel vm, CheckupDetailViewModel dvm)
        {
            var detailsList = dvm.CheckupDetailViewModels.Where(c => c != null).Select(x => x.CheckupDetails).ToList();

            await _mediator.Send(new PerformTreatmentOnPet(vm.OwnerId,
                                                           vm.PetId,
                                                           DateTime.Now.Date,
                                                           vm.Anamnesis,
                                                           vm.ClinicalPicture,
                                                           vm.Diagnosis,
                                                           vm.LabAnalysis,
                                                           vm.Advice,
                                                           vm.NextControlCheckup,
                                                           detailsList,
                                                           vm.PaidSum,
                                                           vm.Therapy));

            var checkupId = await _mediator.Send(new GetLatestCheckupForPet(vm.OwnerId, vm.PetId, CheckupCategory.Treatment));
            return RedirectToAction("Report", "Reports", new { checkupId, vm.OwnerId, vm.PetId, MedicalRecordType.CheckupReport });

        }

        private async Task<CheckupDetailViewModel> GetPartialVM()
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
