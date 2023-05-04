using VDMJasminka.Core.Entities;
using VDMJasminka.Core.Interfaces;

namespace VDMJasminka.Core.Ambulance.Services
{
    public class CheckupItemInventoryService : ICheckupItemInventoryService
    {
        private readonly IReadOnlyRepository<MedicamentInventory> _inventoryRepository;
        private readonly IInventoryCheck _inventoryCheck;

        public CheckupItemInventoryService(IReadOnlyRepository<MedicamentInventory> inventoryRepository, IInventoryCheck inventoryCheck)
        {
            _inventoryRepository = inventoryRepository;
            _inventoryCheck = inventoryCheck;
        }

        public bool IsItemOnOrBelowMininalAmount(int medicamentId, double amount)
        {
            var item = _inventoryRepository.GetAsync(medicamentId).Result;
            var amountToBeRetrievedFromInventory = item.CurrentAmount - amount;
            bool isOnMinimalAmount = amountToBeRetrievedFromInventory == item.MinimalAmount;
            bool isBelowMinimal = amountToBeRetrievedFromInventory >= 1 && amountToBeRetrievedFromInventory < item.MinimalAmount;
            return isOnMinimalAmount || isBelowMinimal;
        }

        public bool IsItemOutOfStock(int medicamentId)
        {
            var currentAmount = _inventoryCheck.GetInventoryAmountAsync(medicamentId).Result;
            return currentAmount <= 0;
        }

        public bool WillItemBeOutOfStock(int medicamentId, double amountToBeWithdrawn)
        {
            var currentAmount = _inventoryCheck.GetInventoryAmountAsync(medicamentId).Result - amountToBeWithdrawn;
            return currentAmount <= 0;
        }
    }
}