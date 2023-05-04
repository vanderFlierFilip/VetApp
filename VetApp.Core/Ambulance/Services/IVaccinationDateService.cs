using System;
using System.Threading.Tasks;

namespace VDMJasminka.Core.Ambulance.Services
{
    public interface IVaccinationDateService
    {
        Task<DateTime> GetNextVaccinationDateForPet(int ownerId, int petId);
    }
}