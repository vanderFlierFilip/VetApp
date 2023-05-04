using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Commands;
using VDMJasminka.Application.Ambulance.Queries;
using VDMJasminka.Application.Dtos;
using VDMJasminka.Logic.Constants;
using VDMJasminka.WebClient.Models;

namespace VDMJasminka.WebClient.Controllers.Ambulance
{
    public class VaccinationController : Controller
    {
        private readonly IMediator _mediator;
        public VaccinationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IActionResult> Vaccination(int ownerId, int petId)
        {
            var vaccineList = new List<SelectListItem>
            {
                new SelectListItem { Text = "Против заразни болести", Value = "0" },
                new SelectListItem { Text = "Против беснило", Value = "1" }
            };

            var petVM = await _mediator.Send(new GetPetWithOwnerQuery(ownerId, petId));
            var vm = new VaccinationViewModel()
            {
                Pet = petVM.Pet,
                Owner = petVM.Owner,
                OwnerId = ownerId,
                PetId = petId,
                Vaccines = vaccineList,
                CheckupDetailViewModel = await GetPartialVM()
            };

            return View(vm);
        }

        public IActionResult AddCheckupDetailPartial()
        {

            //var medApplications = _ambulatoryService.GetMedicamentApplications()
            //        .Select(x => new SelectListItem
            //        { Text = x.ToString(), Value = x.ToString() });

            //var medsSelectList = _ambulatoryService.GetMedicamentsEnumerable()
            //   .Select(x => new SelectListItem { Text = x.PetName, Value = x.PetId.ToString() });

            //var partialVm = new CheckupDetailViewModel
            //{
            //    Medicaments = medsSelectList.ToList(),
            //    MedApplications = medApplications.ToList(),
            //    CheckupDetailViewModels = new List<CheckupDetailViewModel>()
            //};

            return PartialView("_ChekupMedicamentsPartial");
        }

        public async Task<IActionResult> SaveVaccination(VaccinationViewModel vm, CheckupDetailViewModel dvm)
        {
            var detailsList = dvm.CheckupDetailViewModels
                .Where(c => c != null)
                .Select(x => x.CheckupDetails).ToList();
            var paidSum = vm.PaidSum ?? 0;
            await _mediator.Send(new VaccinateOwnersPet(vm.OwnerId, vm.PetId, vm.Vaccine, paidSum, detailsList, vm.NextVaccinationDate));
            var checkupId = await _mediator.Send(new GetLatestCheckupForOwnerAndPetQuery(vm.OwnerId, vm.PetId));
            return RedirectToAction("Report", "Reports", new { checkupId, vm.OwnerId, vm.PetId });
        }

        public IActionResult GetFile(int id)
        {
            //var file = _ambulatoryService.GetPdfAccountOfCheckup(id);

            return Ok("application/pdf");
        }

        public SelectListItem ToSelectListItem(dynamic value)
        {
            return new SelectListItem { Text = value.Name, Value = value.Id.ToString() };
        }

        [HttpGet]
        public IActionResult GetMedicamentUom(int id)
        {
            //var med = _ambulatoryService.GetMedicamentsEnumerable()
            //.Where(x => x.PetId == id).FirstOrDefault();
            return Ok();
        }

        [HttpGet]
        public IActionResult GetOwnersPets(int ownerId)
        {
            //var pets = _ownersAndPetsService.GetPetsFromOwner(ownerId);
            //pets.Select(x => _petMapper.FromPetDtoToPetModel(x))
            return Json(new { Number = 1 });
        }
        private CreateCheckupDto GetCheckupDto(VaccinationViewModel vm, CheckupDetailViewModel dvm)
        {
            var checkupDetailsVms = dvm.CheckupDetailViewModels
                .Select(c => c.CheckupDetails).ToList();

            var checkupDetailsDtoList = checkupDetailsVms
                .Select(x => new CheckupDetailsDto
                {
                    MedicamentId = x.MedicamentId,
                    Uom = x.Uom,
                    Amount = x.Amount,
                    MedicamentApplication = x.MedicamentApplication,
                    Price = x.Price,
                }).ToList();

            return new CreateCheckupDto
            {
                PetId = vm.PetId,
                CheckupType = "Vakcinacija",
                PaidSum = vm.PaidSum,
                Vaccine = ParseVaccine(vm.Vaccine),
                CheckupDetails = checkupDetailsDtoList,
            };
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

        private string ParseVaccine(string vaccine)
        {
            return vaccine == "0" ? VaccineType.Vaccine : VaccineType.Rabies;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}