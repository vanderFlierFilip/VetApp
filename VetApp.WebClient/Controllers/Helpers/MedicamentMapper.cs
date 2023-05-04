using VDMJasminka.Core.Inventory.MedicamentAggregate;
using VDMJasminka.Core.Inventory.ValueObjects;
using VDMJasminka.WebClient.Models;

namespace VDMJasminka.WebClient.Controllers.Helpers
{
    public class MedicamentMapper
    {
        public MedicamentMapper()
        {

        }
        public Medicament FromModelToMedicament(MedicamentModel model)
        {
            var medDetails = new MedicamentDetails(model.Name, model.Uom, model.Price, model.MinimalAmount, model.Material);
            return new Medicament(medDetails);

        }

        public MedicamentModel FromMedicamentToModel(Medicament medicament)
        {
            return new MedicamentModel
            {
                Id = medicament.Id,
                Name = medicament.MedicamentDetails.Name,
                Price = medicament.MedicamentDetails.Price,
                Uom = medicament.MedicamentDetails.Uom,
                MinimalAmount = (short)medicament.MedicamentDetails.MinimalAmount,
                Material = (bool)medicament.MedicamentDetails.IsMaterial
            };
        }

    }
}