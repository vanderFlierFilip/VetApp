using System.Threading.Tasks;
using VDMJasminka.Core.Interfaces;

namespace VDMJasminka.Core.Ambulance.ACL
{
    public class InventoryCheck : IInventoryCheck
    {
        private readonly IMedicamentCurrentInventoryAmountService _service;

        public InventoryCheck(IMedicamentCurrentInventoryAmountService service)
        {
            _service = service;
        }

        public async Task<double> GetInventoryAmountAsync(int medicamentId)
        {
            var inventoryItem = await _service.GetCurrentAmountInInventoryAsync(medicamentId);
            return inventoryItem.current_amount;
        }
    }
}