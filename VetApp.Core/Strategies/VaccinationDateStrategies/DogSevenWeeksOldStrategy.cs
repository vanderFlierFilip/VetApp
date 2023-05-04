using System;
using VDMJasminka.Core.Ambulance.OwnerAggregate;

namespace VDMJasminka.Core.Strategies.VaccinationDateStrategies
{
    public class DogSevenWeeksOldStrategy : INextVaccinationDateStrategy
    {
        public bool IsApplicable(int weeks) => weeks >= 7 && weeks <= 9;

        public DateTime GetNextVaccinationDate()
        {
            return DateTime.Today.AddDays(3 * 7);
        }

    }
}
