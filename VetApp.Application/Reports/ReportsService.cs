using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System;

namespace VDMJasminka.Application.Reports
{
    public class ReportsService
    {
        public byte[] GetDocument()
        {
            return new MedicalRecordDocument().GeneratePdf();
        }
    }

    public class MedicalRecordDocument : IDocument
    {
        public void Compose(IDocumentContainer container)
        {
            container
             .Page(page =>
             {
                 page.Margin(50);

                 page.Header().Element(ComposeHeader);
                 page.Content().Element(ComposeContent);

                 page.Footer().AlignCenter().Text(x =>
                 {
                     x.CurrentPageNumber();
                     x.Span(" / ");
                     x.TotalPages();
                 });
             });
        }

        private void ComposeHeader(IContainer container)
        {
            var titleStyle = TextStyle.Default.FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

            container.Row(row =>
            {
                row.RelativeItem().Column(column =>
                {
                    column.Item().Text($"Invoice #22").Style(titleStyle);

                    column.Item().Text(text =>
                    {
                        text.Span("Issue date: ").SemiBold();
                        text.Span($"{DateTime.Now:d}");
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("Due date: ").SemiBold();
                        text.Span($"{DateTime.Now:d}");
                    });
                });

                row.ConstantItem(100).Height(50).Placeholder();
            });
        }

        private void ComposeContent(IContainer container)
        {
            container
                .PaddingVertical(40)
                .Height(250)
                .Background(Colors.Grey.Lighten3)
                .AlignCenter()
                .AlignMiddle()
                .Text("Content").FontSize(16);
        }

        public DocumentMetadata GetMetadata()
        {
            return DocumentMetadata.Default;
        }
    }
}