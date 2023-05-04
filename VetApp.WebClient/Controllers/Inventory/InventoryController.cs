using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Queries;
using VDMJasminka.Application.Dtos.Inventory;
using VDMJasminka.Application.Inventory;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Inventory.MedicamentAggregate;
using VDMJasminka.Core.Inventory.SupplierAggregate;
using VDMJasminka.WebClient.Controllers.Helpers;
using VDMJasminka.WebClient.Models;
using X.PagedList;

namespace VDMJasminka.WebClient.Controllers.Inventory
{
    public class InventoryController : Controller
    {
        private readonly ILogger<InventoryController> _logger;
        private readonly IInventoryService _inventory;
        private readonly IRepository<Medicament> _medicamentRepo;
        private readonly IRepository<Supplier> _suppliersRepo;
        private readonly MedicamentMapper _medMapper;
        private readonly IMediator _mediator;


        public InventoryController(ILogger<InventoryController> logger, IInventoryService inventory, IRepository<Medicament> medsRepo, IRepository<Medicament> medicamentRepo, IRepository<Supplier> suppliersRepo, IMediator mediator)
        {
            _logger = logger;
            _inventory = inventory;
            _medMapper = new MedicamentMapper();
            _medicamentRepo = medicamentRepo;
            _suppliersRepo = suppliersRepo;
            _mediator = mediator;
        }

        public IActionResult Inventory(string searchString, int pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }

            var inventoryStateModel = _inventory.GetCurrentInventoryState();


            if (!string.IsNullOrEmpty(searchString))
            {
                //inventoryStateModel = inventoryStateModel.Where(s => s.Medicament.PetName.ToUpper().Contains(searchString.ToUpper()));
            }

            ViewBag.Search = searchString;

            int pageSize = 10;

            ViewBag.PageList = inventoryStateModel.ToPagedList(pageNumber, pageSize);


            return View(inventoryStateModel);
        }

        public IActionResult InventoryEntry()
        {
            //    var suppliersSelectList = _suppliersRepo.GetAllAsync()
            //            .Select(x => new SelectListItem { Text = x.PetName, Value = x.PetId.ToString() })
            //            .ToList();

            //var model = new ResupplyDetailsViewModel
            //{
            //    Medicaments = _medicamentRepo.GetAllAsync()
            //        .Select(x => new SelectListItem { Text = x.PetName, Value = x.PetId.ToString() })
            //    .ToList()
            //};
            //var vm = new InventoryEntryViewModel
            //{
            //    ResupplyDetailsViewModel = model,
            //    SuppliersList = suppliersSelectList
            //};

            return View();
        }

        public ActionResult AddMorePartialView()
        {
            ////this  action page is support cal the partial page.
            //var model = new ResupplyDetailsViewModel
            //{
            //    Medicaments = _medicamentRepo.GetAllAsync()
            //       .Select(x => new SelectListItem { Text = x.PetName, Value = x.PetId.ToString() })
            //       .ToList()
            //};
            //var vm = new InventoryEntryViewModel
            //{
            //    ResupplyDetailsViewModel = model,
            //};
            return PartialView("_AddMorePartialView");
            //^this is actual partical page we have 
            //create on this page in Home Controller as given below image
        }

        private async Task<List<SelectListItem>> GetMedSelectList()
        {
            var res = await _medicamentRepo.GetAll();
            return res
                            .Select(x => new SelectListItem { Text = x.MedicamentDetails.Name, Value = x.Id.ToString() })
                            .ToList();
        }

        public async Task<ActionResult> PostAddMore(InventoryEntryViewModel inventory, ResupplyDetailsViewModel details)
        {
            var dto = new CreateInventoryEntryDto
            {
                SupplierId = inventory.SupplierId,
                Description = inventory.Description,
                Date = inventory.EntryDate,
                Details = details.ResupplyDetailsList.Select(d => new CreateInventoryEntryDetailsDto
                {
                    MedicamentId = (int)d.ResupplyDetails.MedicamentId,
                    Amount = d.ResupplyDetails.Amount,
                    AdditionalInfo = d.ResupplyDetails.AdditionalInfo,
                    Price = d.ResupplyDetails.Price,
                }).ToList(),
            };

            await _inventory.MakeNewInventoryEntry(dto);

            return RedirectToAction("MedicamentInventory", new { pageNumber = 1 });
        }

        [HttpGet]
        public async Task<ActionResult> GetMedicamentPrice(int medicamentId)
        {
            var med = await _mediator.Send(new GetSingleMedicamentByIdQuery(medicamentId));
            return Ok(med.Price);
        }

        [HttpGet]
        public async Task<ActionResult> GetMedicamentTotalPriceAsync(int medicamentId, int amount)
        {
            var med = await _mediator.Send(new GetSingleMedicamentByIdQuery(medicamentId));
            return Ok(amount * med.Price);
        }

        // [HttpGet]
        // public ActionResult CreateNewMedicament()
        // {
        //     string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

        //     var vm = new CreateMedicamentModalViewModel
        //     {
        //         ControllerName = controllerName
        //     };
        //     return PartialView("_MedicamentModal", vm);
        // }

        [HttpPost]
        public async Task<ActionResult> CreateMedicament(MedicamentModel vm)
        {
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            await _medicamentRepo.Add(_medMapper.FromModelToMedicament(vm));

            return Ok();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}