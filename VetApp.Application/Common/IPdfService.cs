using VDMJasminka.Shared.Constants;

namespace VDMJasminka.Application.Common
{
    public interface IPdfService
    {
        byte[] GenerateMedicalRecordPdf(string pdfContentTemplate, MedicalRecordType recordType);
    }

}
