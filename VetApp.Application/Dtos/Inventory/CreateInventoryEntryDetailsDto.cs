using System;
using System.Collections.Generic;
using System.Text;

namespace VDMJasminka.Application.Dtos.Inventory
{
    public class CreateInventoryEntryDetailsDto
    {
        public int MedicamentId { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
