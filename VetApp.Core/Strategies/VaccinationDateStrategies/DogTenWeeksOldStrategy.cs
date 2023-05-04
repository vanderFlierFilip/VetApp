using System;
using VDMJasminka.Core.Ambulance.OwnerAggregate;

namespace VDMJasminka.Core.Strategies.VaccinationDateStrategies
{
    public class DogTenWeeksOldStrategy : INextVaccinationDateStrategy
    {
        public bool IsApplicable(int weeks) => weeks >= 10 && weeks <= 50;
        public DateTime GetNextVaccinationDate()
        {
            return DateTime.Today.AddDays(3 * 7);
        }
    }
}
