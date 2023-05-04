using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Queries;
using VDMJasminka.Shared.Dto.Ambulance;
using VDMJasminka.Shared.Requests;
using VDMJasminka.WebApi.Core.Routes;

namespace VDMJasminka.WebApi.Controllers.Ambulance
{
    public class MedicalRecordsController : BaseAmbulanceController
    {
        private IMediator _mediator;

        public MedicalRecordsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.Ambulance.GetCheckupTypeMedicalRecord)]
        public async Task<ActionResult<List<CheckupDto>>> GetMedicalRecordForPet([FromRoute] int petId, [FromQuery] MedicalRecordsParameters parameters)
        {
            var result = await _mediator.Send(new GetCheckupTypeMedicalRecord(petId, parameters));
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Ambulance.GetOwnerAndPetMedicalRecordInformation)]
        public async Task<ActionResult<List<CheckupDto>>> GetOwnerAndPetInformation([FromRoute] int petId, [FromRoute] int ownerId)
        {
            var result = await _mediator.Send(new GetPetWithOwnerQuery(petId, ownerId));
            return Ok(result);
        }
    }
}