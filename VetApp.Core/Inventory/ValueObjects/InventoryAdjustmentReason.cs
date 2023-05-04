using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Inventory.ValueObjects
{
    public class InventoryAdjustmentReason : ValueObject
    {
        public InventoryAdjustmentReason(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
        public string Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
