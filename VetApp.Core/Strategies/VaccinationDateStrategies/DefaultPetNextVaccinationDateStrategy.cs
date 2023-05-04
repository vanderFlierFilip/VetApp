using System;
using VDMJasminka.Core.Ambulance.OwnerAggregate;

namespace VDMJasminka.Core.Strategies.VaccinationDateStrategies
{
    public class DefaultPetNextVaccinationDateStrategy : INextVaccinationDateStrategy
    {
        public bool IsApplicable(int weeks) => weeks >= 52;

        public DateTime GetNextVaccinationDate()
        {
            return DateTime.Today.AddYears(1);
        }
    }
}