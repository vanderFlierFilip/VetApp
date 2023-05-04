using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Queries;

namespace VDMJasminka.WebClient.ViewComponents
{
    public class ReportViewComponent : ViewComponent
    {

        private readonly IMediator _mediator;

        public ReportViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync(int ownerId, int petId, int checkupId)
        {
            var vm = await _mediator.Send(new GetCheckupReportQuery(ownerId, petId, checkupId));

            return View(vm);
        }
    }
}
