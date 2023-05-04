using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
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
    public class ChipInsertionsController : BaseAmbulanceController
    {
        private readonly IMediator _mediator;

        public ChipInsertionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.Ambulance.GetVeterinaryTreatmentViewModel)]
        public async Task<ActionResult<CreateCheckupViewModel>> Get([FromRoute] int ownerId, [FromRoute] int petId)
        {
            var vm = await _mediator.Send(new GetCreateCheckupViewModel(ownerId, petId));

            return Ok(vm);
        }

        [HttpPost(ApiRoutes.Ambulance.SaveVeterinaryTreatment)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<CheckupDto>> SaveChipInsertion([FromBody] CreateChipInsertionDto dto)
        {
            await _mediator.Send(new PerformChipInsertionCheckupOnPet(dto.OwnerId,
                                                                      dto.PetId,
                                                                      System.DateTime.Now,
                                                                      dto.ChipNumber,
                                                                      dto.PaidSum,
                                                                      dto.CheckupItems));

            var checkup = await _mediator.Send(new GetLatestCheckupForOwnerAndPetQuery(dto.OwnerId, dto.PetId));
            return Ok(checkup);
        }
    }
}