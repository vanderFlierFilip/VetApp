using System;
using System.Collections.Generic;
using System.Text;

namespace VDMJasminka.Core.Finances.Entities
{
    public class MiscellaneousExpense
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public MiscellaneousExpenseType ExpenseType { get; set; }
    }
}