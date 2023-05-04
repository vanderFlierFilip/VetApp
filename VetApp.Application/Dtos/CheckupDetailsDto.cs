using System;
using System.Collections.Generic;
using System.Text;

namespace VDMJasminka.Application.Dtos
{
    public class CheckupDetailsDto
    {
        public int? CheckupId { get; set; }
        public int MedicamentId { get; set; }
        public double? Amount { get; set; }
        public string Uom { get; set; }
        public string MedicamentApplication { get; set; }
        public double? Price { get; set; }
    }
}
