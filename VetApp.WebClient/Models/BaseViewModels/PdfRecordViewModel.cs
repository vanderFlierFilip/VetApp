using System.Collections.Generic;
using VDMJasminka.Shared.Dto.Ambulance;

namespace VDMJasminka.WebClient.Models.BaseViewModels
{
    public class PdfRecordViewModel : BasePdfRecordViewModel
    {
        public List<CheckupDetailsDto> CheckupDetails { get; set; }
    }
}
