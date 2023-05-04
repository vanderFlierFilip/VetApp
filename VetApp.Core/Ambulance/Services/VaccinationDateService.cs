using System;
using System.Threading.Tasks;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Factory;
using VDMJasminka.Core.Interfaces;

namespace VDMJasminka.Core.Ambulance.Services
{
    public class VaccinationDateService : IVaccinationDateService
    {
        private readonly IRepository<Owner> _ownerRepository;
        private readonly INextVaccinationDateFactory _factory;

        public VaccinationDateService(INextVaccinationDateFactory factory, IRepository<Owner> ownerRepository)
        {
            _factory = factory;
            _ownerRepository = ownerRepository;
        }

        public async Task<DateTime> GetNextVaccinationDateForPet(int ownerId, int petId)
        {
            var owner = await _ownerRepository.Get(ownerId);
            var pet = owner.GetPet(petId);

            return _factory.GetNextVaccinationDate(pet.CalculateAgeInWeeks());
        }
    }
}