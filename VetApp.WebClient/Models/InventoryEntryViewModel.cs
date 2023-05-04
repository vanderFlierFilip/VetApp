using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using VDMJasminka.Core.Inventory.SupplierAggregate;

namespace VDMJasminka.WebClient.Models
{
    public class InventoryEntryViewModel
    {
        public int SupplierId { get; set; }
        public List<SelectListItem> SuppliersList {get; set;}
        public ResupplyDetailsViewModel ResupplyDetailsViewModel { get; set; }
        public string Description { get; set; }
        public DateTime EntryDate { get; set; }
    }

    public class ResupplyDetailsViewModel
    {
        public List<SelectListItem> Medicaments {get; set;}
        public SupplierOrderItem ResupplyDetails {get; set;}
        public List<ResupplyDetailsViewModel> ResupplyDetailsList {get; set;}

    }
}