using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Inventory.MedicamentAggregate;
using VDMJasminka.WebClient.Controllers.Helpers;
using VDMJasminka.WebClient.Models;
using X.PagedList;



namespace VDMJasminka.WebClient.Controllers.Ambulance
{
    public class MedicamentsController : Controller
    {
        private readonly ILogger<MedicamentsController> _logger;
        private readonly IRepository<Medicament> _medicamentRepo;
        private readonly MedicamentMapper _medicamentMapper;

        public MedicamentsController(ILogger<MedicamentsController> logger, IRepository<Medicament> medicamentRepo)
        {
            _logger = logger;
            _medicamentRepo = medicamentRepo;
            _medicamentMapper = new MedicamentMapper();
        }

        public async Task<IActionResult> MedicamentsAsync(int pageNumber, string searchString)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            var medicaments = await _medicamentRepo.GetAll();

            if (!string.IsNullOrEmpty(searchString))
            {
                medicaments = medicaments.Where(s => s.MedicamentDetails.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            ViewBag.Search = searchString;

            int pageSize = 5;
            ViewBag.PageList = medicaments.ToPagedList(pageNumber, pageSize);

            return View(medicaments);
        }

        public IActionResult MedicamentDetails(int medicamentId)
        {
            return View(_medicamentRepo.Get(medicamentId));
        }
        [HttpGet]
        public IActionResult CreateMedicament()
        {
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

            var model = new CreateMedicamentModalViewModel()
            {
                ControllerName = controllerName
            };
            return PartialView("_MedicamentModal", model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateMedicament(CreateMedicamentModalViewModel model)
        {
            var medicament = _medicamentMapper.FromModelToMedicament(model.Medicament);

            await _medicamentRepo.Add(medicament);

            return RedirectToAction("Medicaments", new { pageNumber = 1, searchString = model.Medicament.Name });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteMedicament(int id)
        {
            await _medicamentRepo.Delete(await _medicamentRepo.Get(id));
            return RedirectToAction("Medicaments", new { pageNumber = 1 });

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}