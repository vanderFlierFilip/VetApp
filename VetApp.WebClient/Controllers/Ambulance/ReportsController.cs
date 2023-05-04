using MediatR;
using Microsoft.AspNetCore.Mvc;
using RazorLight;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Application.Ambulance.Commands;
using VDMJasminka.Application.Ambulance.Queries;
using VDMJasminka.Shared.Constants;
using VDMJasminka.WebClient.Models.BaseViewModels;

namespace VDMJasminka.WebClient.Controllers.Ambulance
{
    public class ReportsController : Controller
    {

        private readonly IMediator _mediator;
        public MedicalRecordType RecordType { get; set; }
        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<ActionResult> ReportAsync(int checkupId, int ownerId, int petId)
        {
            var vm = await _mediator.Send(new GetCheckupReportQuery(ownerId, petId, checkupId));

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> DownloadReport(int ownerId, int petId, int checkupId)
        {
            var vm = await _mediator.Send(new GetCheckupReportQuery(ownerId, petId, checkupId));

            var pdfVm = new SurgeryReportViewModel()
            {
                Owner = vm.Owner,
                Pet = vm.Pet,
                PaidSum = vm.Checkup.PaidSum,
                ClinicalPicture = vm.Checkup.ClinicalPicture,
                CheckupDetails = vm.Checkup.CheckupDetails.ToList(),
                NextControlCheckup = vm.Checkup.NextControlCheckup,
                NextVaccinationDate = vm.Checkup.NextVaccinationDate,
                Advice = vm.Checkup.Advice,
                Anamnesis = vm.Checkup.Anamnesis,
                SurgicalIntervention = vm.Checkup.SurgicalIntervention,
            };

            MedicalRecordType rt = MedicalRecordType.MedicalRecord;
            if (vm.Checkup.CheckupType == CheckupCategory.Surgery)
            {
                rt = MedicalRecordType.SurgicalReport;
            }
            if (vm.Checkup.CheckupType == CheckupCategory.Chipping)
            {
                rt = MedicalRecordType.ChipInsertionReport;
            }
            if (vm.Checkup.CheckupType == CheckupCategory.Treatment)
            {
                rt = MedicalRecordType.CheckupReport;
            }
            if (vm.Checkup.CheckupType == CheckupCategory.Vaccine)
            {
                rt = MedicalRecordType.VaccinationReport;
            }

            var template = await GetTemplateStringAsync(pdfVm);
            var pdf = await _mediator.Send(new GeneratePDFFromTemplateString(vm.Owner.Id, vm.Pet.Id, template, rt, null));
            var fileName = $"{vm.Checkup.CheckupType}-{vm.Pet.Name}-{DateTime.Now.ToShortDateString()}";

            return File(pdf, "application/pdf", fileName + ".pdf");
        }

        private async Task<string> GetTemplateStringAsync(SurgeryReportViewModel vm)
        {
            var project = Path.Combine(Directory.GetCurrentDirectory(), "Views");
            var engine = new RazorLightEngineBuilder()
                 .UseFileSystemProject(project)
                 .UseMemoryCachingProvider()
                 .Build();

            return await engine.CompileRenderAsync("Shared/PDFReports/SurgeryReportPDF.cshtml", vm);
        }
    }
}
