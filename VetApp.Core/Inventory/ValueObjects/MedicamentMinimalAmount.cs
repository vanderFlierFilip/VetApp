using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Inventory.ValueObjects
{
    public class MedicamentMinimalAmount : ValueObject
    {
        private MedicamentMinimalAmount() { }
        public int Value { get;}

        public MedicamentMinimalAmount(int minimalAmount)
        {
            if (minimalAmount < 0) throw new ArgumentOutOfRangeException(nameof(minimalAmount));
            Value = minimalAmount;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
