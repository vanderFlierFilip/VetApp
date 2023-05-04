using System;
using System.Collections.Generic;
using System.Text;

namespace VDMJasminka.Core.Finances.Entities
{
    public class SupplierExpense
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int SupplierId { get; set; }
        public decimal Amount { get; set; }
    }
}