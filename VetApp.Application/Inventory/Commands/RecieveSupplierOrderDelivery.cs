using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace VDMJasminka.Application.Inventory.Commands
{
    public class RecieveSupplierOrderDelivery : IRequest
    {
        public RecieveSupplierOrderDelivery(int supplierId, RecieveOrderFromSupplierDto delivery)
        {
            SupplierId = supplierId;
            Delivery = delivery;
        }

        public int SupplierId { get; }
        public RecieveOrderFromSupplierDto Delivery { get; }
    }

    public class RecieveOrderFromSupplierDto
    {
        public int SupplierId { get; set; }
        public string Description { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }

    public class OrderItemDto
    {
        public int MedicamentId { get; set; }
        public decimal Price { get; set; }
        public decimal Amount { get; set; }
        public string AdditionalInfo { get; set; }
    }
}