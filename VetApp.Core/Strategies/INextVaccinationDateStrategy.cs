using System;
using VDMJasminka.Core.Ambulance.OwnerAggregate;

namespace VDMJasminka.Core.Strategies
{
    public interface INextVaccinationDateStrategy
    {
        bool IsApplicable(int weeks);

        DateTime GetNextVaccinationDate();
    }
}