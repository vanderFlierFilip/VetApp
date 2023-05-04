using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Queries;

namespace VDMJasminka.WebClient.ViewComponents
{
    public class SearchPetViewComponent : ViewComponent
    {
        private readonly IMediator _mediator;

        public SearchPetViewComponent(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var vm = await _mediator.Send(new GetPetsWithOwnerListQuery());

            return View(vm);
        }

    }
}
