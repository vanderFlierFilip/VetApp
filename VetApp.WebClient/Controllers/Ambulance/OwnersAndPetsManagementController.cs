using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Commands;
using VDMJasminka.Application.Ambulance.Queries;
using VDMJasminka.Core;
using VDMJasminka.Shared.Dto.Ambulance;
using VDMJasminka.WebClient.Controllers.Helpers;
using VDMJasminka.WebClient.Extensions;
using VDMJasminka.WebClient.MappingExtensions;
using VDMJasminka.WebClient.Models;
using X.PagedList;

namespace VDMJasminka.WebClient.Controllers.Ambulance
{
    public class OwnersAndPetsManagementController : Controller
    {
        public IEnumerable<SpeciesDto> Species { get; private set; }

        private readonly PetMapper _petMapper;
        private readonly OwnerMapper _ownerMapper;
        private readonly IMediator _mediator;

        public PartialViewResult Partial { get; private set; }

        public OwnersAndPetsManagementController(IMediator mediator)
        {
            _petMapper = new PetMapper();
            _ownerMapper = new OwnerMapper();
            _mediator = mediator;
            Species = _mediator.Send(new GetAllSpeciesQuery()).Result;
        }

        public async Task<IActionResult> OwnersAsync(int pageNumber, string searchString, string sortOrder)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParam = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";

            ViewBag.SearchString = searchString;

            ViewBag.Search = searchString;

            ViewBag.PageNumber = pageNumber;

            ViewBag.PageList = await GetPaginatedOwnersAsync(pageNumber, searchString, sortOrder);

            return View();
        }

        public async Task<IPagedList<OwnerModel>> GetPaginatedOwnersAsync(int? pageNumber, string searchString, string sortOrder)
        {
            var ownerModels = await GetOwnerModelsAsync();

            switch (sortOrder)
            {
                case "name_desc":
                    ownerModels = ownerModels.OrderByDescending(o => o.FullName);
                    break;

                default:
                    ownerModels = ownerModels.OrderBy(o => o.FullName);
                    break;
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                ownerModels = ownerModels.Where(s => s.FullName.ToUpper().Contains(searchString.ToUpper()));
            }

            int pageSize = 5;
            return ownerModels.ToPagedList(pageNumber ?? 1, pageSize);
        }

        public IActionResult CreateOwner()
        {
            var owner = new CreateOwnerMultiFormViewModel
            {
            };

            return PartialView("_OwnerModal", owner);
        }

        public async Task<IActionResult> GetCreatePetPartialAsync()
        {
            var model = new PetMultiFormModel()
            {
                AnimalSexOptions = new List<SelectListItem>
                {
                    new SelectListItem {Text="M", Value="M"},
                    new SelectListItem {Text="Ж", Value="Ž"}
                },
                SpeciesList = await GetSpeciesAsSelectList(),
            };

            return PartialView("_CreatePet", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOwner(CreateOwnerMultiFormViewModel m, PetMultiFormModel vm)
        {
            var pets = vm.PetModels.Select(x => x.Pet).Where(x => x != null).Select(x => new CreatePetDto
            {
                Name = x.Name,
                BreedId = x.BreedId,
                Alergy = x.Alergy,
                ChipNumber = x.ChipNumber,
                LastCheckup = x.LastCheckup,
                DateOfBirth = x.DateOfBirth,
                Description = x.Description,
                Sex = x.Sex,
                SpeciesId = x.SpeciesId
            }).ToList();

            var ownerId = await _mediator.Send(new CreateOwner(m.OwnerModel.FullName,
                                                               m.OwnerModel.Address,
                                                               m.OwnerModel.Location,
                                                               m.OwnerModel.PhoneNumber,
                                                               m.OwnerModel.Municipality,
                                                               m.OwnerModel.SSN,
                                                               null,
                                                               m.OwnerModel.AdditionalInfo,
                                                               m.OwnerModel.Email,
                                                               pets));

            return RedirectToAction("Owners", new { pageNumber = 1 });
        }

        [HttpPost]
        public IActionResult DeleteOwner(int ownerId)
        {
            return RedirectToAction("Owners", new { pageNumber = 1 });
        }

        public async Task<IActionResult> OwnerDetails(int id)
        {
            var ownerModel = await GetOwnerModelAsync(id);
            return View(ownerModel);
        }

        public async Task<ActionResult> EditOwnerAsync(int ownerId)
        {
            var ownerModel = await GetEditOwnerViewModelAsync(ownerId);
            return View(ownerModel);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOwner(EditOwnerViewModel viewModel)
        {
            var ownerModel = viewModel.Owner;
            await _mediator.Send(new UpdateOwnerLivingAddress(ownerModel.Id,
                                                              ownerModel.Address,
                                                              ownerModel.Location,
                                                              ownerModel.Municipality));

            return RedirectToAction("EditOwner", new { ownerId = viewModel.Owner.Id });
        }

        public async Task<IActionResult> AddNewPet(int ownerId)
        {
            var model = new PetModel { OwnerId = ownerId };

            var vm = new AddPetViewModel
            {
                Pet = model,
                AnimalSexOptions = new List<SelectListItem>
                {
                    new SelectListItem {Text="M", Value="M"},
                    new SelectListItem {Text="Ж", Value="Ž"}
                },
                SpeciesList = await GetSpeciesAsSelectList(),
            };

            return PartialView("_AddPetPartialView", vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewPet(AddPetViewModel vm)
        {
            if (ModelState.IsValid && TryValidateModel(vm.Pet))
            {
                await _mediator.Send(new RegisterPetToOwner(vm.Pet.OwnerId,
                                                            vm.Pet.Name,
                                                            vm.Pet.SpeciesId,
                                                            vm.Pet.BreedId,
                                                            vm.Pet.DateOfBirth,
                                                            vm.Pet.Sex,
                                                            vm.Pet.Description,
                                                            vm.Pet.Alergy,
                                                            vm.Pet.ChipNumber));
            }

            return RedirectToAction("EditOwner", new { ownerId = vm.Pet.OwnerId });
        }

        public Task<List<SelectListItem>> GetSpeciesAsSelectList()
        {
            var species = Species;

            return Task.FromResult(species
                .Select(t => new SelectListItem
                {
                    Value = t.Id.ToString(),
                    Text = t.Name.TranslateAnimalType()
                })
                .ToList());
        }

        [HttpGet]
        public async Task<JsonResult> GetSpeciesBreedsAsSelectList(int speciesId)
        {
            var species = await _mediator.Send(new GetAllSpeciesQuery());

            var selectedSpecies = species.FirstOrDefault(species => species.Id == speciesId);

            var breeds = selectedSpecies.Breeds.Select(b => new { b.Id, b.Name }).ToList();

            return Json(breeds);
        }

        [HttpGet]
        public ActionResult GetBreeds(int specieId)
        {
            var breeds = Species.Where(x => x.Id == specieId).SelectMany(x => x.Breeds);
            return Ok(breeds);
        }

        private async Task<IEnumerable<OwnerModel>> GetOwnerModelsAsync()
        {
            var result = await _mediator.Send(new GetOwnersQuery());

            var ownerModelList = result.Select(x => x.ToModel());
            return ownerModelList.Select(o =>
            {
                return new OwnerModel
                {
                    Id = o.Id,
                    FullName = o.FullName,
                    AdditionalInfo = o.AdditionalInfo,
                    Address = o.Address,
                    Email = o.Email,
                    Location = o.Location,
                    Municipality = o.Municipality,
                    PhoneNumber = o.PhoneNumber,
                    SSN = o.SSN,
                    Pets = o.Pets.Select(async p =>
                    {
                        var petSpecies = await GetSpeciesForPet(p.SpeciesId);
                        return new PetModel
                        {
                            PetId = p.PetId,
                            Name = p.Name,
                            ChipNumber = p.ChipNumber,
                            DateOfBirth = p.DateOfBirth,
                            Sex = p.Sex,
                            Description = p.Description,
                            Alergy = p.Alergy,
                            LastCheckup = p.LastCheckup,
                            Species = petSpecies,
                            Breed = petSpecies.Breeds.Single(b => b.Id == p.BreedId)
                        };
                    }).Select(x => x.Result)
                };
            });
        }

        private async Task<Species> GetSpeciesForPet(int speciesId)
        {
            return await _mediator.Send(new GetSingleSpeciesByIdQuery(speciesId));
        }

        private async Task<EditOwnerViewModel> GetEditOwnerViewModelAsync(int id)
        {
            var owner = await _mediator.Send(new GetSingleOwnerByIdQuery(id));

            var species = await _mediator.Send(new GetAllSpeciesQuery());

            return new EditOwnerViewModel
            {
                Owner = owner,
                AnimalTypes = species.Select(at => new Species { Id = (int)at.Id, Type = at.Name })
            };
        }

        private async Task<CreateOwnerDto> GetOwnerModelAsync(int id)
        {
            return await _mediator.Send(new GetSingleOwnerByIdQuery(id));
        }
    }
}