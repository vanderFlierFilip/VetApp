namespace VDMJasminka.Core.Ambulance.Services
{
    public interface ICheckupItemInventoryService
    {
        bool IsItemOnOrBelowMininalAmount(int medicamentId, double amount);

        bool IsItemOutOfStock(int medicamentId);

        bool WillItemBeOutOfStock(int medicamentId, double amountToBeWithdrawn);
    }
}