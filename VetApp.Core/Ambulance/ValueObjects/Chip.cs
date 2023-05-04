using System.Collections.Generic;
using VDMJasminka.Core.Ambulance.Exceptions;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Ambulance.ValueObjects
{
    public class Chip : ValueObject
    {
        private const short CORRECT_CHIPNO_LENGTH = 15;

        public string Number { get; set; }

        public Chip(string number)
        {
            if (!string.IsNullOrEmpty(number) && IsInvalid(number))
            {
                throw new ChipNumberIsNotCorrectLengthException($"Chip number length not correct! Should be: {CORRECT_CHIPNO_LENGTH}, got: {number.Length}");
            }

            Number = number;
        }

        private bool IsInvalid(string chipNumberValue)
        {
            return chipNumberValue.Length > CORRECT_CHIPNO_LENGTH;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}