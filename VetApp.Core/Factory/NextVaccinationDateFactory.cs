using System;
using System.Collections.Generic;
using System.Linq;
using VDMJasminka.Core.Strategies;

namespace VDMJasminka.Core.Factory
{
    public class NextVaccinationDateFactory : INextVaccinationDateFactory
    {
        private readonly IEnumerable<INextVaccinationDateStrategy> _strategies;
        public NextVaccinationDateFactory(IEnumerable<INextVaccinationDateStrategy> strategies)
        {
            _strategies = strategies;
        }

        public DateTime GetNextVaccinationDate(int weeks)
        {
            var strategy = _strategies.FirstOrDefault(s => s.IsApplicable(weeks));
            return strategy.GetNextVaccinationDate();

        }
    }
}
