using MediatR;

namespace VDMJasminka.Application.Inventory.Commands
{
    public class CreateNewMedicament : IRequest<int>
    {
        public CreateNewMedicament(string name, string uom, double price, int minimalAmount, bool isMaterial)
        {
            Name = name;
            Uom = uom;
            Price = price;
            MinimalAmount = minimalAmount;
            IsMaterial = isMaterial;
        }

        public string Name { get; }
        public string Uom { get; }
        public double Price { get; }
        public int MinimalAmount { get; }
        public bool IsMaterial { get; }
    }
}