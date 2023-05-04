using System.ComponentModel.DataAnnotations;

namespace VDMJasminka.Shared.Dto.Ambulance
{
    public class CheckupDetailsDto
    {
        public int MedicamentId { get; set; }
        public double? Amount { get; set; }
        public string Uom { get; set; }
        public string Application { get; set; }
        public string Name { get; set; }
    }
}