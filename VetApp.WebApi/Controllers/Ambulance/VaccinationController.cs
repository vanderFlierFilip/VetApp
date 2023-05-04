using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Commands;
using VDMJasminka.Application.Ambulance.Queries;
using VDMJasminka.Shared.Dto.Ambulance;
using VDMJasminka.Shared.ViewModels;
using VDMJasminka.WebApi.Core.Filters;
using VDMJasminka.WebApi.Core.Routes;

namespace VDMJasminka.WebApi.Controllers.Ambulance
{
    [ServiceFilter(typeof(RefreshTokenSetterAttribute))]
    public class VaccinationController : BaseAmbulanceController
    {
        private readonly IMediator _mediator;

        public VaccinationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.Ambulance.GetVeterinaryTreatmentViewModel)]
        public async Task<ActionResult<CreateVaccinationViewModel>> Vaccination(int ownerId, int petId)
        {
            var petVM = await _mediator.Send(new GetPetWithOwnerQuery(ownerId, petId));
            var vm = new CreateVaccinationViewModel()
            {
                Pet = petVM.Pet,
                Owner = petVM.Owner,
            };

            return Ok(vm);
        }

        [HttpPost(ApiRoutes.Ambulance.SaveVeterinaryTreatment)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<CheckupDto>> SaveVaccination([FromBody] CreateVaccinationDto dto)
        {
            await _mediator.Send(new VaccinateOwnersPet(dto.OwnerId, dto.PetId, dto.Vaccine, dto.PaidSum, dto.CheckupItems, dto.NextVaccinationDate));
            var checkupDto = await _mediator.Send(new GetLatestCheckupForOwnerAndPetQuery(dto.OwnerId, dto.PetId));
            return Ok(checkupDto);
        }
    }
}