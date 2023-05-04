using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorLight;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Commands;
using VDMJasminka.Application.Ambulance.Queries;
using VDMJasminka.Shared.Constants;
using VDMJasminka.Shared.ViewModels;
using VDMJasminka.WebClient.Models;

namespace VDMJasminka.WebClient.Controllers.Ambulance
{
    public class PetDetailsPageController : Controller
    {
        private readonly IMediator _mediator;

        public PetDetailsPageController(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<IActionResult> PetDetailsAsync(int ownerId, int petId)
        {
            // GetAsync pet with owner information
            var vm = await _mediator.Send(new GetPetWithOwnerQuery(ownerId, petId));

            return View(vm);
        }

        [HttpPost]
        public JsonResult FuckOffToPetDetials(int ownerId, int petId)
        {
            return Json(Url.Action("PetDetails", new { ownerId, petId }));
        }

        [HttpGet]
        public async Task<IActionResult> GetEditOwnerModal(int ownerId, int petId)
        {
            var ownerDto = await _mediator.Send(new GetSingleOwnerByIdQuery(ownerId));
            var vm = new EditOwnerModalViewModel()
            {
                Owner = ownerDto,
                PetId = petId
            };
            return PartialView("_EditOwnerModal", vm);
        }

        [HttpGet]
        public async Task<IActionResult> GetEditPetModal(int ownerId, int petId)
        {
            var petDto = await _mediator.Send(new GetSinglePetFromOwnerQuery(ownerId, petId));
            var species = await _mediator.Send(new GetAllSpeciesQuery());

            var vm = new EditPetModalViewModel()
            {
                OwnerId = petDto.OwnerId,
                Pet = petDto,
                SpeciesList = species.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList(),
                Breeds = species.SelectMany(x => x.Breeds).Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList(),
            };

            return PartialView("_EditPetModal", vm);
        }
        public async Task<IActionResult> DownloadPdf(int ownerId, int petId)
        {
            var vm = await _mediator.Send(new GetPetWithOwnerQuery(ownerId, petId));

            var template = await GetMedicalRecordTemplateAsStringAsync(vm);

            var pdf = await _mediator.Send(new GeneratePdfCheckupReport(ownerId, petId, template, MedicalRecordType.MedicalRecord, DateTime.Now));

            return File(pdf, "application/pdf", DateTime.Now.ToString() + ".pdf");
        }

        public async Task<IActionResult> ChangeOwnerInformation(EditOwnerModalViewModel model)
        {
            await _mediator.Send(new UpdateOwnerLivingAddress(model.Owner.Id,
                                                              model.Owner.Address,
                                                              model.Owner.Location,
                                                              model.Owner.Municipality));

            return RedirectToAction("PetDetails", new { ownerId = model.Owner.Id, petId = model.PetId });
        }

        public async Task<IActionResult> ChangePetInformation(EditPetModalViewModel model)
        {
            await _mediator.Send(new ChangePetDescriptionAndAlergy(model.OwnerId,
                                                               model.Pet.Id,
                                                               model.Pet.Name,
                                                               model.Pet.DateOfBirth,
                                                               model.Pet.Sex,
                                                               model.Pet.Description,
                                                               model.Pet.Alergy));

            return RedirectToAction("PetDetails", new { ownerId = model.OwnerId, petId = model.Pet.Id });
        }
        private async Task<string> GetMedicalRecordTemplateAsStringAsync(PetDetailsViewModel vm)
        {
            var project = Path.Combine(Directory.GetCurrentDirectory(), "Views");
            var engine = new RazorLightEngineBuilder()
                 .UseFileSystemProject(project)
                 .UseMemoryCachingProvider()
                 .Build();

            return await engine.CompileRenderAsync("Shared/_MedicalRecordPdf.cshtml", vm);
        }


    }
}
