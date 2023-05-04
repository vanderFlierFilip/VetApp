using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using VDMJasminka.Application.Inventory.Commands;
using VDMJasminka.Application.Inventory.Queries;
using VDMJasminka.Shared.Dto.Inventory;
using VDMJasminka.Shared.Queries;
using VDMJasminka.Shared.Requests;
using VDMJasminka.WebApi.Core.Routes;
using static VDMJasminka.Application.Inventory.Commands.RecieveSupplierOrderDelivery;

namespace VDMJasminka.WebApi.Controllers.Inventory
{
    public class SuppliersController : BaseInventoryController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<InventoryController> _logger;

        public SuppliersController(IMediator mediator, ILogger<InventoryController> logger, IHttpContextAccessor accessor)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(ApiRoutes.Warehouse.GetSuppliers)]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<PagedList<SupplierDto>>> GetSuppliers([FromQuery] RequestParameters parameters)
        {
            var paging = new PageInfo() { PageSize = parameters.PageSize, PageIndex = parameters.PageNumber };
            var result = await _mediator.Send(new GetSuppliers() { Paging = paging, RequestParameters = parameters });
            return Ok(result);
        }

        [HttpGet(ApiRoutes.Warehouse.GetSingleSupplier)]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<PagedList<SupplierDto>>> GetSupplier([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetSupplierDetailsQuery(id));
            return Ok(result);
        }

        [HttpPost(ApiRoutes.Warehouse.RecieveOrder)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<int>> RecieveOrderFromSupplier([FromRoute] int id, [FromBody] RecieveOrderFromSupplierDto command)
        {
            await _mediator.Send(new RecieveSupplierOrderDelivery(id, command));
            return Accepted(command);
        }
    }
}