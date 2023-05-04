using System;
using System.Collections.Generic;
using System.Text;

namespace VDMJasminka.Core.Finances.Entities
{
    public class Income
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal IncomeAmount { get; set; }
        public string IncomeType { get; set; }
    }
}