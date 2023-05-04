using System;
using System.Collections.Generic;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Ambulance.ValueObjects
{
    public class PetInfo : ValueObject
    {
        public PetInfo(string name, DateTime? dateOfBirth, string sex, string description, string alergy)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            DateOfBirth = dateOfBirth;
            Sex = sex;
            Description = description;
            Alergy = alergy;
        }

        public string Name { get; private set; }
        public DateTime? DateOfBirth { get; private set; }

        public string Sex { get; private set; }

        public string Description { get; private set; }

        public string Alergy { get; private set; }

        public void UpdateDescriptionAndAlergy(string description, string alergy)
        {
            Description = description;
            Alergy = alergy;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return DateOfBirth;
            yield return Sex;
            yield return Description;
            yield return Alergy;
        }
    }
}