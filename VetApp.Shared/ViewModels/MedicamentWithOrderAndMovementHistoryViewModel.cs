using System;
using System.Collections.Generic;
using System.Text;

namespace VDMJasminka.Shared.ViewModels
{
    public class MedicamentWithOrderAndMovementHistoryViewModel
    {
        public MedicamentInventoryViewModel Medicament { get; set; }
        public IEnumerable<MedicamentHistoryOrderDto> Orders { get; set; }
        public IEnumerable<MedicamentInventoryWithrawalDto> Withdrawals { get; set; }
        public IEnumerable<dynamic> Adjustments { get; set; }
    }
    public class MedicamentInventoryWithrawalDto
    {
        public int Id { get; set; }
        public string WithdrawalType { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }

    }
    public class MedicamentHistoryOrderDto
    {
        public int SupplyReceptionId { get; set; }
        public string OrderNo { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
    }
}
