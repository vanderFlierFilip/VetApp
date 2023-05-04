using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using RazorLight;
using System.IO;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Queries;
using VDMJasminka.Core.Entities;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.WebApi.Core.Filters;
using VDMJasminka.WebApi.Core.Routes;
using QuestPDF.Infrastructure;
using VDMJasminka.Application.Reports;

namespace VDMJasminka.WebApi.Controllers.Ambulance
{
    [ServiceFilter(typeof(RefreshTokenSetterAttribute))]
    public class PetsController : BaseAmbulanceController
    {
        private readonly IMediator _mediator;
        private readonly IReadOnlyRepository<MedicamentInventory> _repo;

        public PetsController(IMediator mediator, IReadOnlyRepository<MedicamentInventory> repo)
        {
            _mediator = mediator;
            _repo = repo;
        }

        [HttpGet(ApiRoutes.Ambulance.GetVeterinaryTreatmentViewModel)]
        public async Task<ActionResult<dynamic>> PetDetailsAsync(int ownerId, int petId)
        {
            await using var stream = new MemoryStream();
            var docService = new ReportsService();
            var pdf = docService.GetDocument();
            var vm = await _mediator.Send(new GetPetWithOwnerQuery(ownerId, petId));
            return File(pdf, "application/pdf", "myReport.pdf");
        }

        //public async Task<IActionResult> DownloadPdf(int ownerId, int petId)
        //{
        //    var vm = await _mediator.Send(new GetPetWithOwnerQuery(ownerId, petId));

        //    var template = await GetMedicalRecordTemplateAsStringAsync(vm);

        //    var pdf = await _mediator.Send(new GeneratePdfCheckupReportHandler(ownerId, petId, template, MedicalRecordType.MedicalRecord, DateTime.Now));

        //    return File(pdf, "application/pdf", DateTime.Now.ToString() + ".pdf");
        //}

        private async Task<string> GetMedicalRecordTemplateAsStringAsync(Shared.ViewModels.PetDetailsViewModel vm)
        {
            var project = Path.Combine(Directory.GetCurrentDirectory(), "Views");
            var engine = new RazorLightEngineBuilder()
                 .UseFileSystemProject(project)
                 .UseMemoryCachingProvider()
                 .Build();

            return await engine.CompileRenderAsync("Shared/_MedicalRecordPdf.cshtml", vm);
        }
    }
}