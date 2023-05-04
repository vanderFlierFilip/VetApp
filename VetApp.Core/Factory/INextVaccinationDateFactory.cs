using System;

namespace VDMJasminka.Core.Factory
{
    public interface INextVaccinationDateFactory
    {
        DateTime GetNextVaccinationDate(int weeks);
    }
}