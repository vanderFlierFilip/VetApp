using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDMJasminka.Application.Inventory.Queries
{
    public class GetSupplierDetailsQuery : IRequest<SupplierDetailsViewModel>
    {
        public GetSupplierDetailsQuery(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }

    public class SupplierDetailsViewModel
    {
        public double Balance { get; set; }
        public List<Product> Products { get; set; }
        public List<RecievedOrder> RecievedOrders { get; set; }
    }

    public class RecievedOrder
    {
        public DateTime Date { get; set; }
        public string Description { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}