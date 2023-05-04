using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Commands;
using VDMJasminka.Application.Ambulance.Queries;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Shared.Dto.Ambulance;
using VDMJasminka.Shared.ViewModels;
using VDMJasminka.WebApi.Core.Filters;
using VDMJasminka.WebApi.Core.Routes;

namespace VDMJasminka.WebApi.Controllers.Ambulance
{
    public class SurgeriesController : BaseAmbulanceController
    {
        private readonly IMediator _mediator;

        public SurgeriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.Ambulance.GetVeterinaryTreatmentViewModel)]
        public async Task<ActionResult<CreateCheckupViewModel>> Surgery(int ownerId, int petId)
        {
            var vm = await _mediator.Send(new GetCreateCheckupViewModel(ownerId, petId));

            return Ok(vm);
        }

        [HttpPost(ApiRoutes.Ambulance.SaveVeterinaryTreatment)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<ActionResult<int>> SaveSurgery([FromBody] CreateSurgeryDto dto)
        {
            await _mediator.Send(new PerformSurgeryOnOwnersPet(dto.OwnerId,
                                                     dto.PetId,
                                                     DateTime.Now,
                                                     dto.Anamnesis,
                                                     dto.ClinicalPicture,
                                                     dto.Diagnosis,
                                                     dto.SurgicalIntervention,
                                                     dto.LabAnalysis,
                                                     dto.Advice,
                                                     dto.NextControlCheckup,
                                                    dto.CheckupItems,
                                                     dto.PaidSum,
                                                      dto.Therapy));

            var checkup = await _mediator.Send(new GetLatestCheckupForOwnerAndPetQuery(dto.OwnerId, dto.PetId));
            return Ok(checkup);
        }
    }
}