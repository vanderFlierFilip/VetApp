using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Inventory.MedicamentAggregate;
using VDMJasminka.Core.Inventory.SupplierAggregate;
using VDMJasminka.WebClient.Controllers.Helpers;
using VDMJasminka.WebClient.Models;
using X.PagedList;

namespace VDMJasminka.WebClient.Controllers
{
    public class ManagementController : Controller
    {
        private readonly IRepository<Owner> _ownersRepo;
        private readonly IOwnersManagementService _ownersService;
        private readonly IRepository<Pet> _petsRepository;
        private readonly IRepository<Supplier> _suppliersRepo;
        private readonly PetMapper _petMapper;
        private readonly OwnerMapper _ownerMapper;
        private readonly SupplierMapper _supplierMapper;
        private readonly IRepository<Medicament> _medicamentsRepo;

        public ManagementController(IRepository<Owner> ownersRepo, IOwnersManagementService ownersService, IRepository<Pet> petsRepository, IRepository<Supplier> suppliersRepo, IRepository<Medicament> medicamentRepository)
        {
            _ownersRepo = ownersRepo;
            _ownersService = ownersService;
            _petsRepository = petsRepository;
            _petMapper = new PetMapper();
            _ownerMapper = new OwnerMapper();
            _supplierMapper = new SupplierMapper();
            _suppliersRepo = suppliersRepo;
            _medicamentsRepo = medicamentRepository;
        }

        public async Task<IActionResult> SuppliersAsync(int pageNumber, string searchString, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "name_asc";

            ViewBag.SearchString = searchString;
            if (searchString != null)
            {
                pageNumber = 1;
            }

            var suppliers = await _suppliersRepo.GetAll();
            switch (sortOrder)
            {
                case "name_desc":
                    suppliers = suppliers.OrderByDescending(s => s.Name);
                    break;
                default:
                    suppliers = suppliers.OrderBy(s => s.Name);
                    break;
            }
            if (!string.IsNullOrEmpty(searchString))
            {
                suppliers = suppliers.Where(s => s.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            ViewBag.PageNum = pageNumber;
            int pageSize = 5;
            ViewBag.PageList = suppliers.ToPagedList(pageNumber, pageSize);

            return View(suppliers);
        }
        [HttpGet]
        public IActionResult CreateSupplier()
        {
            var supplier = new SupplierModel();
            return PartialView("_SupplierModal", supplier);
        }
        [HttpPost]
        public IActionResult CreateSupplier(SupplierModel supplierModel)
        {
            _suppliersRepo.Add(_supplierMapper.FromSupplierModelToSupplier(supplierModel));
            return RedirectToAction("Suppliers", new { pageNumber = 1, searchString = supplierModel.Name });

        }
        public async Task<IActionResult> EditSupplierAsync(int supplierId)
        {
            var supplier = await _suppliersRepo.Get(supplierId);
            return View(_supplierMapper.FromSupplierToSupplierModel(supplier));
        }
        [HttpPost]
        public IActionResult EditSupplier(SupplierModel supplierModel)
        {
            _suppliersRepo.Update(_supplierMapper.FromSupplierModelToSupplier(supplierModel));
            return RedirectToAction("Suppliers", new { pageNumber = 1, searchString = supplierModel.Name });
        }

        [HttpPost]
        public async Task<ActionResult> DeleteSupplierAsync(int id)
        {
            var supplier = await _suppliersRepo.Get(id);

            if (supplier != null)
            {
                _suppliersRepo.Delete(supplier);

                return RedirectToAction("Suppliers", new { pageNumber = 1 });
            }

            return NotFound();
        }

    }

}
