using System.Collections.Generic;
using System.Threading.Tasks;
using VDMJasminka.Application.Dtos.Inventory;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Inventory.MedicamentAggregate;
using VDMJasminka.Core.Inventory.SupplierAggregate;

namespace VDMJasminka.Application.Inventory
{
    public class InventoryService : IInventoryService
    {

        private readonly IRepository<Medicament> _medicamentsRepo;

        public InventoryService(IRepository<Medicament> medicamentsRepo)
        {
            _medicamentsRepo = medicamentsRepo;
        }

        public IEnumerable<InventoryStateObject> GetCurrentInventoryState()
        {
            var totalEntries = GetTotalEntriesFromInventoryQuery();
            var totalWithdrawals = GetTotalWithdrawalsFromInventoryQuery();
            return GetInventoriesWithTotalAmountQuery(totalWithdrawals, totalEntries);
        }

        public IEnumerable<InventoryStateObject> GetItemsWithAlarmingQuantity()
        {
            return new List<InventoryStateObject>();
            //return GetCurrentInventoryState()
            //    .Where(i => i.Medicament.Value > i.CurrentAmountInInventory
            //    && i.Medicament.Value == i.CurrentAmountInInventory + 1);
        }

        private IEnumerable<dynamic> GetTotalWithdrawalsFromInventoryQuery()
        {
            return new List<dynamic>();
            //return from checkup in checkupDetails
            //       group checkup by checkup.MedicamentId into g
            //       select new
            //       {
            //           MedicamentId = g.Key,
            //           TotalAmount = g.Sum(x => x.Amount)
            //       };
        }

        private IEnumerable<dynamic> GetTotalEntriesFromInventoryQuery()
        {
            //var resupplies = _resuppliesRepo.GetAllAsync();
            //var resupplyDetails = _resupplyDetailsRepo.GetAllAsync();
            return new List<dynamic>();
            //return from resupply in resupplyDetails
            //       join e in resupplies on resupply.SupplierOrderId equals e.PetId
            //       group resupply by resupply.MedicamentId into g
            //       select new
            //       {
            //           MedicamentId = g.Key,
            //           TotalAmount = g.Sum(x => x.Amount),
            //           Supplier = g.Select(x => x).First()
            //       };
        }

        private IEnumerable<InventoryStateObject> GetInventoriesWithTotalAmountQuery(IEnumerable<dynamic> withdrawals, IEnumerable<dynamic> entries)
        {
            var medicaments = _medicamentsRepo.GetAll();
            return new List<InventoryStateObject>();
            //return
            //        from med in medicaments
            //        join e in entries on med.PetId equals e.MedicamentId
            //        join w in withdrawals on med.PetId equals w.MedicamentId into ulist
            //        from u in ulist.DefaultIfEmpty()
            //        orderby med.PetId
            //        select new InventoryStateObject
            //        {
            //            Medicament = med,
            //            CurrentAmountInInventory = e.TotalAmount - (u == null ? 0 : (decimal)u.TotalAmount),
            //            Supplier = e.Supplier
            //        };
        }

        public async Task MakeNewInventoryEntry(CreateInventoryEntryDto dto)
        {
            //var resupply = new  SupplierOrder
            //{
                //Date = dto.Date,
                //InvoiceNumber = dto.InvoiceNumber,
                //SupplierId = dto.SupplierId,
                //Items = dto.Details.Select(d => new MedicamentInventory
                //{
                //    Amount = d.Amount,
                //    Price = d.Price,
                //    AdditionalInfo = d.AdditionalInfo,
                //    MedicamentId = d.MedicamentId,
                //}).ToList()
            //};
        }
    }
}