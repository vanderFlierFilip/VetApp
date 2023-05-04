using Dapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using VDMJasminka.Application.Inventory.Commands;
using VDMJasminka.Application.Inventory.Queries;
using VDMJasminka.Shared.Queries;
using VDMJasminka.Shared.Requests;
using VDMJasminka.Shared.ViewModels;
using VDMJasminka.WebApi.Core;
using VDMJasminka.WebApi.Core.Filters;
using VDMJasminka.WebApi.Core.Routes;

namespace VDMJasminka.WebApi.Controllers.Inventory
{
    [ServiceFilter(typeof(RefreshTokenSetterAttribute))]
    public class InventoryController : BaseInventoryController
    {
        private readonly IMediator _mediator;
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(IMediator mediator, ILogger<InventoryController> logger, IHttpContextAccessor accessor)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet(ApiRoutes.Warehouse.GetInventoryItems)]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<PagedList<MedicamentInventoryViewModel>>> GetAsync([FromQuery] InventoryParameters parameters)
        {
            try
            {
                var paging = new PageInfo() { PageSize = parameters.PageSize, PageIndex = parameters.PageNumber };
                var result = await _mediator.Send(new GetInventoryStatusQuery()
                {
                    Paging = paging,
                    InventoryParameters = parameters
                });

                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return new ObjectResult(new ApiException(500, e.Message, e.StackTrace));
            }
        }

        [HttpGet(ApiRoutes.Warehouse.GetSingleItemFromInventory)]
        public async Task<ActionResult<MedicamentWithOrderAndMovementHistoryViewModel>> GetSingleItemFromInventory([FromRoute] int id)
        {
            var result = await _mediator.Send(new GetSingleMedicamentWithOrderAndMovementHistory(id));
            return Ok(result);
        }

        [HttpPost(ApiRoutes.Warehouse.AddItem)]
        public async Task<ActionResult> AddItemToInventory([FromBody] CreateMedicamentDetailsDto dto)
        {
            var commandId = await _mediator.Send(new CreateNewMedicament(dto.Name, dto.Uom, dto.Price, dto.MinimalAmount, dto.IsMaterial));
            return CreatedAtAction(nameof(GetSingleItemFromInventory), new { id = commandId }, commandId);
        }

        [HttpPatch(ApiRoutes.Warehouse.PatchItemPrice)]
        public async Task<ActionResult> PatchInventoryItemPrice([FromRoute] int id, [FromBody] PatchMedicamentPrice dto)
        {
            await _mediator.Send(new ChangeMedicamentPrice(id, dto.NewPrice));
            return Accepted(id);
        }

        [HttpPatch(ApiRoutes.Warehouse.PatchMinimalAmountOfItem)]
        public async Task<ActionResult> PatchMinimalAmountOfItem([FromRoute] int id, [FromBody] PatchMedicamentMinimalAmount dto)
        {
            await _mediator.Send(new ChangeMedicamentMinimalAmountInStock(id, dto.Amount));
            return Accepted(id);
        }

        [HttpPatch(ApiRoutes.Warehouse.PatchItemDetails)]
        public async Task<ActionResult> PatchItemDetails([FromRoute] int id, [FromBody] ChangeMedicamentDetailsDto dto)
        {
            await _mediator.Send(new ChangeMedicamentDetails(id, dto.Name, dto.Uom, dto.IsMaterial));
            return Accepted(id);
        }
    }

    public class CreateSupplierEntryDto
    {
        public int SupplierId { get; set; }
        public string Description { get; set; }
        public List<CreateInventoryItemDto> Items { get; set; }
    }

    public class CreateInventoryItemDto
    {
        public int MedicamentId { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public string AdditionalInfo { get; set; }
    }

    public class PatchMedicamentMinimalAmount
    {
        public int Amount { get; set; }
    }

    public class PatchMedicamentPrice
    {
        public double NewPrice { get; set; }
    }

    public class ChangeMedicamentDetailsDto
    {
        [Required, NotNull]
        public string Name { get; set; }

        [Required, NotNull]
        public string Uom { get; set; }

        [Required, NotNull]
        public bool IsMaterial { get; set; }
    }

    public class CreateMedicamentDetailsDto
    {
        [Required, NotNull]
        public string Name { get; set; }

        [Required, NotNull]
        public string Uom { get; set; }

        [Required, NotNull]
        public bool IsMaterial { get; set; }

        [Required, NotNull]
        public double Price { get; set; }

        public int MinimalAmount { get; set; }
    }
}