using MediatR;
using System.Collections.Generic;
using VDMJasminka.Shared.Dto;
using VDMJasminka.Shared.Queries;
using VDMJasminka.Shared.Requests;
using VDMJasminka.Shared.ViewModels;

namespace VDMJasminka.Application.Inventory.Queries
{
    public class GetInventoryStatusQuery : IRequest<PagedList<MedicamentInventoryViewModel>>
    {
        public PageInfo Paging { get; set; }
        public InventoryParameters InventoryParameters { get; set; }
    }
}
