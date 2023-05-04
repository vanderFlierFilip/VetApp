using System.Threading.Tasks;

namespace VDMJasminka.Core.Interfaces
{
    public interface IMedicamentCurrentInventoryAmountService
    {
        Task<dynamic> GetCurrentAmountInInventoryAsync(int id);
    }
}