using System.Threading.Tasks;

namespace VDMJasminka.Core.Interfaces
{
    public interface IInventoryCheck
    {
        Task<double> GetInventoryAmountAsync(int medicamentId);
    }
}