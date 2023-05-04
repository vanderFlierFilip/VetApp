using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Commands;
using VDMJasminka.Application.Ambulance.Queries;
using VDMJasminka.Application.Inventory.Queries;
using VDMJasminka.Shared.Dto.Ambulance;
using VDMJasminka.Shared.Queries;
using VDMJasminka.Shared.Requests;
using VDMJasminka.Shared.ViewModels;
using VDMJasminka.WebApi.Core;
using VDMJasminka.WebApi.Core.Filters;
using VDMJasminka.WebApi.Core.Routes;
using static Slapper.AutoMapper;

namespace VDMJasminka.WebApi.Controllers.Ambulance
{
    [ServiceFilter(typeof(RefreshTokenSetterAttribute))]
    public class OwnersController : BaseAmbulanceController
    {
        private readonly IMediator _mediator;

        public OwnersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets a paged list of Owners.
        /// </summary>
        /// <param name="pageNumber">The page number to retrieve.</param>
        /// <param name="pageSize">The number of items per page.</param>
        /// <returns>A paged list of Owners.
        /// 200 OK
        /// 400 Bad Request </returns>
        /// <response code="200">if the request is successful.</response>
        /// <response code="400">If there is an error processing the request.</response>
        ///
        [HttpGet(ApiRoutes.Ambulance.GetPetOwners)]
        public async Task<ActionResult<PagedList<CreateOwnerDto>>> GetOwnersAsync([FromQuery] RequestParameters parameters)
        {
            var paging = new PageInfo() { PageSize = parameters.PageSize, PageIndex = parameters.PageNumber };
            var result = await _mediator.Send(new GetOwnersQuery
            {
                Paging = paging,
                RequestParameters = parameters
            });

            return Ok(result);
        }

        [HttpGet(ApiRoutes.Ambulance.GetOwner)]
        public async Task<ActionResult> GetSingleOwner([FromRoute] int id)
        {
            var owner = await _mediator.Send(new GetSingleOwnerByIdQuery(id));
            return Ok(owner);
        }

        [HttpPost(ApiRoutes.Ambulance.RegisterNewOwnerWithPets)]
        public async Task<ActionResult<int>> PostOwnerAsync([FromBody] CreateOwnerDto dto)
        {
            var result = await _mediator.Send(new RegisterNewOwnerWithPets(dto.FullName, dto.Address, dto.Location, dto.PhoneNumber, dto.Municipality, dto.SSN, dto.AdditionalInfo, dto.Email, dto.Pets));

            return CreatedAtAction("GetSingleOwner", new { id = result }, result);
        }
    }
}