using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Commands;
using VDMJasminka.Application.Ambulance.Queries;
using VDMJasminka.Shared.Dto.Ambulance;
using VDMJasminka.Shared.ViewModels;
using VDMJasminka.WebApi.Controllers.Ambulance;
using VDMJasminka.WebApi.Core.Filters;
using VDMJasminka.WebApi.Core.Routes;

namespace VDMJasminka.WebApi.Abulance.Controllers
{
    [ServiceFilter(typeof(RefreshTokenSetterAttribute))]
    public class CheckupsController : BaseAmbulanceController
    {
        private readonly IMediator _mediator;

        public CheckupsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet(ApiRoutes.Ambulance.GetVeterinaryTreatmentViewModel)]
        public async Task<ActionResult<CreateCheckupViewModel>> Get(int ownerId, int petId)
        {
            var vm = await _mediator.Send(new GetCreateCheckupViewModel(ownerId, petId));

            return Ok(vm);
        }

        [HttpPost(ApiRoutes.Ambulance.SaveVeterinaryTreatment)]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> SaveCheckup([FromBody] CreateCheckupDto dto)
        {
            await _mediator.Send(new PerformTreatmentOnPet(dto.OwnerId,
                                                           dto.PetId,
                                                           DateTime.Now,
                                                           dto.Anamnesis,
                                                           dto.ClinicalPicture,
                                                           dto.Diagnosis,
                                                           dto.LabAnalysis,
                                                           dto.Advice,
                                                           dto.NextControlCheckup,
                                                           dto.CheckupItems,
                                                           dto.PaidSum,
                                                           dto.Therapy));

            var checkupId = await _mediator.Send(new GetLatestCheckupForOwnerAndPetQuery(dto.OwnerId, dto.PetId));

            return Ok(checkupId);
            //return RedirectToAction("Report", "Reports", new { checkupId, dto.OwnerId, dto.PetId, MedicalRecordType.CheckupReport });
        }
    }
}