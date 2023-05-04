using System.Threading.Tasks;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Inventory.SupplierAggregate;
using VDMJasminka.Shared.Dto.Inventory;

namespace VDMJasminka.Application.Inventory
{
    public class SupplierService
    {
        private readonly IRepository<Supplier> _repository;

        public SupplierService(IRepository<Supplier> repository)
        {
            _repository = repository;
        }

        public async Task<SupplierDto> CreateSupplierAsync(SupplierDto dto)
        {
            var entity = await _repository.Add(new Supplier(dto.Name, dto.StreetAddress, dto.Location, dto.PhoneNumber));

            return new SupplierDto
            {
                Id = entity.Id,
                Name = entity.Name,
                PhoneNumber = entity.Address.PhoneNumber,
                Location = entity.Address.Location,
                StreetAddress = entity.Address.Address
            };
        }
    }
}