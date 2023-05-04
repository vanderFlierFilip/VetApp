using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Inventory.ValueObjects
{
    public class MedicamentPrice : ValueObject
    {
        public MedicamentPrice(double value) 
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value));

            Value = value;
        }

        public double Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
