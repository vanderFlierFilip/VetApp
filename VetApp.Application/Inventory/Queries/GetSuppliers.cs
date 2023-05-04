using MediatR;
using System.Collections.Generic;
using VDMJasminka.Shared.Dto.Inventory;
using VDMJasminka.Shared.Queries;
using VDMJasminka.Shared.Requests;

namespace VDMJasminka.Application.Inventory.Queries
{
    public class GetSuppliers : IRequest<PagedList<SupplierDto>>
    {
        public PageInfo Paging { get; set; }
        public RequestParameters RequestParameters { get; set; }
    }
}