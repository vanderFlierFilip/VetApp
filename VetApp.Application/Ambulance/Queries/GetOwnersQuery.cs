using MediatR;
using System.Collections.Generic;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Shared.Queries;
using VDMJasminka.Shared.Requests;
using VDMJasminka.Shared.ViewModels;

namespace VDMJasminka.Application.Ambulance.Queries
{
    public class GetOwnersQuery : IRequest<PagedList<OwnerListViewModel>>
    {
        public PageInfo Paging { get; set; }
        public RequestParameters RequestParameters { get; set; }
    }
}