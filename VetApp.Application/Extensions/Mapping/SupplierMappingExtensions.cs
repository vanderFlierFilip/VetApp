using VDMJasminka.Core.Inventory.SupplierAggregate;
using VDMJasminka.Shared.Dto.Inventory;

namespace VDMJasminka.Application.Extensions.Mapping
{
    public static class SupplierMappingExtensions
    {
        public static SupplierDto ToDto(this Supplier supplier)
        {
            return new SupplierDto()
            {
                Id = supplier.Id,
                Name = supplier.Name,
                StreetAddress = supplier.Address?.Address,
                Location = supplier.Address?.Location,
                PhoneNumber = supplier.Address?.PhoneNumber
            };
        }
    }
}