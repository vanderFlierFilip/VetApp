using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Inventory.ValueObjects
{
    public class MedicamentDetails : ValueObject
    {
        private MedicamentDetails()
        {

        }
        public MedicamentDetails(string name, string uom, bool? isMaterial)
        {
            Name = name;
            Uom = uom;
            IsMaterial = isMaterial;
        }

        public string Name { get; private set; }
        public string Uom { get; private set; }
        public bool? IsMaterial { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Uom; 
            yield return IsMaterial;
        }
    }
}
