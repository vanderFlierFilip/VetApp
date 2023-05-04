using System.IO;
using VDMJasminka.Application.Common;
using VDMJasminka.Shared.Constants;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace VDMJasminka.Application.Services
{

    public class PdfService : IPdfService
    {
        private readonly IConverter _converter;
        public PdfService(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] GenerateMedicalRecordPdf(string pdfContentTemplate, MedicalRecordType recordType)
        {
            var (generalStyleSheetUrl, headerHtmlUrl, footerHtmlUrl) = GetMedicalRecordHtmlAndCssPathsTuple(recordType);
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4Plus,
                ViewportSize = "1024x768",
            },
                Objects =
            {
                  new ObjectSettings()
                  {
                      PagesCount = true,
                    HtmlContent = pdfContentTemplate,
                    WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet=generalStyleSheetUrl},
                    HeaderSettings = { HtmlUrl= headerHtmlUrl},
                    FooterSettings = { HtmlUrl=footerHtmlUrl},

                  }
             }
            };
            byte[] pdf = _converter.Convert(doc);
            return pdf;
        }

        private (string generalStylesPath, string headerHtmlPath, string footerHtmlPath) GetMedicalRecordHtmlAndCssPathsTuple(MedicalRecordType recordType)
        {
            string headerHtmlPath;
            switch (recordType)
            {
                case MedicalRecordType.CheckupReport:
                    headerHtmlPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "pdfstyles", "checkup-report-header.html");
                    break;
                case MedicalRecordType.VaccinationReport:
                    headerHtmlPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "pdfstyles", "vaccination-report-header.html");
                    break;
                case MedicalRecordType.SurgicalReport:
                    headerHtmlPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "pdfstyles", "surgery-report-header.html");
                    break;
                case MedicalRecordType.ChipInsertionReport:
                    headerHtmlPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "pdfstyles", "chip-insertion-header.html");
                    break;
                default:
                    headerHtmlPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "pdfstyles", "medical-record-header.html");
                    break;
            }

            string generalStylesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "lib", "bootstrap", "dist", "css", "custom-minty.css");
            string footerHtmlPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "assets", "pdfstyles", "medical-record-footer.html");

            return (generalStylesPath, headerHtmlPath, footerHtmlPath);
        }


    }
}
