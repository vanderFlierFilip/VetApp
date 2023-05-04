using System.Threading.Tasks;
using VDMJasminka.Core.Entities;
using VDMJasminka.Core.Interfaces;

namespace VDMJasminka.Core.Inventory.Services
{
    public class MedicamentInventoryService : IMedicamentInventoryService {

        private readonly IReadOnlyRepository<MedicamentInventory> _medicamentInventoryRepository;
        public MedicamentInventoryService(IReadOnlyRepository<MedicamentInventory> medicamentInventoryRepository)
        {
            _medicamentInventoryRepository = medicamentInventoryRepository;
        }

        public bool IsMedicamentInStock(int medicamentId)
        {
            var med = _medicamentInventoryRepository.Get(medicamentId);
            return med.CurrentAmount > 0;
        }
    }
}
