using System.Threading.Tasks;

namespace VDMJasminka.Core.Inventory.Services
{
    public interface IMedicamentInventoryService
    {
        bool IsMedicamentInStock(int medicamentId);
    }
}