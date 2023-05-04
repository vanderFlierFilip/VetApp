using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Commands;
using VDMJasminka.Application.Ambulance.Queries;
using VDMJasminka.WebClient.Models;

namespace VDMJasminka.WebClient.Controllers.Ambulance
{
    public class SurgeryController : Controller
    {
        private readonly IMediator _mediator;
        public SurgeryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Surgery(int ownerId, int petId)
        {
            var petVM = await _mediator.Send(new GetPetWithOwnerQuery(ownerId, petId));
            CheckupDetailViewModel partialVm = await GetPartialVM();
            var diagnoses = await _mediator.Send(new GetAllDiagnosesQuery());
            var vm = new SurgeryViewModel()
            {
                Pet = petVM.Pet,
                Owner = petVM.Owner,
                OwnerId = ownerId,
                PetId = petId,
                Diagnoses = diagnoses.Select(s => new SelectListItem { Text = s, Value = s }).ToList(),
                CheckupDetailViewModel = await GetPartialVM()
            };

            return View(vm);
        }


        [HttpGet]
        public async Task<IActionResult> GetMedicamentUom(int id)
        {
            var medicaments = await _mediator.Send(new GetAllMedicamentsQuery());
            var med = medicaments.Where(x => x.Id == id).FirstOrDefault();
            return Ok(med.Uom);
        }
        public async Task<IActionResult> SurgeryReportDetails(int ownerId, int petId)
        {
            var checkupId = await _mediator.Send(new GetLatestCheckupForPet(ownerId, petId, CheckupCategory.Surgery));
            return RedirectToAction("Report", "Reports", new { checkupId, ownerId, petId });
        }

        [HttpPost]
        public async Task<ActionResult> SaveSurgery(SurgeryViewModel vm, CheckupDetailViewModel dvm)
        {
            var detailsList = dvm.CheckupDetailViewModels
                .Where(x => x != null).Select(x => x.CheckupDetails);

            await _mediator.Send(new PerformSurgeryOnOwnersPet(vm.OwnerId,
                                                     vm.PetId,
                                                     DateTime.Now.Date,
                                                     vm.Anamnesis,
                                                     vm.ClinicalPicture,
                                                     vm.Diagnosis,
                                                     vm.SurgicalIntervention,
                                                     vm.LabAnalysis,
                                                     vm.Advice,
                                                     vm.NextControlCheckup,
                                                     detailsList.ToList(),
                                                     vm.PaidSum,
                                                     vm.Therapy));

            return RedirectToAction("SurgeryReportDetails", new { ownerId = vm.OwnerId, petId = vm.PetId });
        }

        public async Task<IActionResult> AddCheckupDetailPartial()
        {

            return PartialView("_CheckupMedicamentsPartial", await GetPartialVM());
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
