using System;
using System.Collections.Generic;
using System.Text;

namespace VDMJasminka.Shared.Requests
{
    public class InventoryParameters : RequestParameters
    {
        public bool? InStock { get; set; }
        public bool? OnOrBelowMinimalAmount { get; set; }
        public bool? IsMaterial { get; set; }
    }
}
