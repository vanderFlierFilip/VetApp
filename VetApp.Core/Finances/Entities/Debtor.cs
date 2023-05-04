using System;

namespace VDMJasminka.Core.Finances.Entities
{
    public class Debtor
    {
        public int Id { get; set; }
        public int OwnerId { get; set; }
        public DateTime DueDate { get; set; }
        public decimal DebtAmount { get; set; }
    }
}