using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Queries;
using VDMJasminka.Shared.Dto.Inventory;
using VDMJasminka.WebClient.Models;

namespace VDMJasminka.WebClient.Controllers.Ambulance
{
    public class CheckupMedicamentsHelperController : Controller
    {
        private readonly IEnumerable<MedicamentDto> _meds;
        private readonly IMediator _mediator;
        public CheckupMedicamentsHelperController(IMediator mediator)
        {
            _mediator = mediator;
            _meds = _mediator.Send(new GetAllMedicamentsQuery()).Result;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetMedicamentPrice(int medicamentId)
        {
            var med = _meds.FirstOrDefault(med => med.Id == medicamentId);
            return Ok(med.Price);
        }

        [HttpGet]
        public ActionResult GetMedicamentUom(int id)
        {
            var med = _meds.Where(x => x.Id == id).FirstOrDefault();
            return Ok(med.Uom);
        }

        [HttpPost]
        public ActionResult GetTotalAmount(SurgeryViewModel vm, CheckupDetailViewModel dvm)
        {

            dvm.CheckupDetailViewModels.RemoveAll(item => item == null);
            var detailsList = dvm.CheckupDetailViewModels.Select(x => x.CheckupDetails);
            var totalAmount = detailsList.Sum(x => x.Price * x.Amount);
            return Ok(Math.Round((decimal)totalAmount, 2));
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
