using VDMJasminka.Core.Inventory.SupplierAggregate;
using VDMJasminka.WebClient.Models;

namespace VDMJasminka.WebClient.Controllers.Helpers
{
    public class SupplierMapper
    {
        public Supplier FromSupplierModelToSupplier(SupplierModel model)
        {
            return new Supplier();

        }

        public SupplierModel FromSupplierToSupplierModel(Supplier supplier)
        {
            return new SupplierModel
            {
                Id = supplier.Id,
                Name = supplier.Name,
                PhoneNumber = supplier.Address.PhoneNumber,
                Location = supplier.Address.Location,
                Address = supplier.Address.Address
            };
        }
    }
}