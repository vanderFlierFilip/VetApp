using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Inventory.SupplierAggregate;

namespace VDMJasminka.Application.Inventory.Commands.Handlers
{
    internal sealed class RecieveSupplierOrderDeliveryHandler : IRequestHandler<RecieveSupplierOrderDelivery>
    {
        private readonly IRepository<Supplier> _repository;

        public RecieveSupplierOrderDeliveryHandler(IRepository<Supplier> repositoruy)
        {
            _repository = repositoruy;
        }

        public async Task<Unit> Handle(RecieveSupplierOrderDelivery request, CancellationToken cancellationToken)
        {
            var supplier = await _repository.Get(request.SupplierId);
            var orderItems = request.Delivery.Items.Select(x => new SupplierOrderItem(x.MedicamentId, x.Price, x.Amount, x.AdditionalInfo)).ToList();
            supplier.RecieveOrderDelivery(DateTime.Now, request.Delivery.Description, orderItems);
            await _repository.Update(supplier);
            return Unit.Value;
        }
    }
}