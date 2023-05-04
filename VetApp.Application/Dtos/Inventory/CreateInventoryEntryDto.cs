using System;
using System.Collections.Generic;
using System.Text;

namespace VDMJasminka.Application.Dtos.Inventory
{
    public class CreateInventoryEntryDto
    {
        public int SupplierId { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public IEnumerable<CreateInventoryEntryDetailsDto> Details { get; set; }
    }
}
