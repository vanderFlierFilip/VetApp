using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Queries;
using VDMJasminka.Logic.Constants;
using VDMJasminka.WebClient.MappingExtensions;
using VDMJasminka.WebClient.Models;

namespace VDMJasminka.WebClient.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var today = DateTime.Today;
            var rangeStart = DateTime.Today.AddDays(1);
            var rangeEnd = DateTime.Today.AddDays(3);

            //var pendingVaccinationsToday = _ambulatoryProtocolService.GetPendingVaccinations()
            //   .Select(v => new PendingVaccination
            //   {
            //       OwnerName = v.Owner.PetName,
            //       OwnerPhoneNumber = v.Owner.PhoneNumber,
            //       PetName = v.PetName,
            //       VaccineType = TranslateVaccineType(v.VaccineType),
            //       Date = v.Date

            //   });
            //var pendingVaccinationsInRage = _ambulatoryProtocolService.GetPendingVaccinations(rangeStart, rangeEnd)
            //     .Select(v => new PendingVaccination
            //   {
            //       OwnerName = v.Owner.PetName,
            //       OwnerPhoneNumber = v.Owner.PhoneNumber,
            //       PetName = v.PetName,
            //       VaccineType = TranslateVaccineType(v.VaccineType),
            //       Date = v.Date

            //   });

            var vm = new HomeViewModel
            {
                PetSearchList = await _mediator.Send(new GetPetsWithOwnerListQuery())
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> GetAppointmentPartial()
        {
            var query = await _mediator.Send(new GetOwnersQuery());
            var ownerModelList = query.Select(o => o.ToModel());
            var vm = new AddAppointmentViewModel
            {
                TypeaheadViewModel = new OwnerAndPetTypeaheadViewModel
                {
                    OwnerModelList = ownerModelList,
                }
            };

            return PartialView("_CreateAppointmentModal", vm);
        }

        [HttpGet]
        public async Task<IActionResult> GetSchedule(DateTime from, DateTime to)
        {
            var schedule = await _mediator.Send(new GetScheduledAppointmentsForDateRange(2, from, to));
            return Ok(schedule);
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetPetsSeachList()
        {
            return Json(await _mediator.Send(new GetPetsWithOwnerListQuery()));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string TranslateVaccineType(string vaccineName)
        {
            if (vaccineName == VaccineType.Rabies)
                return "Против беснило";
            if (vaccineName == VaccineType.Vaccine)
                return "Против заразни болести";

            return "";
        }
    }
}