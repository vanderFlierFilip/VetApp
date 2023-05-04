using AutoFixture;
using Castle.Core.Internal;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Ambulance.Services;
using VDMJasminka.Core.Ambulance.ValueObjects;
using VDMJasminka.Core.Factory;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Strategies;
using VDMJasminka.Core.Strategies.VaccinationDateStrategies;
using Xunit;

namespace VDMJasminka.Test.DomainModelTests.Ambulance
{
    public class NextVaccinationDateDomainServiceTests
    {
        private readonly Fixture _fixture;
        private readonly IVaccinationDateService _sut;
        private readonly Mock<IRepository<Owner>> _ownerRepository;
        private readonly INextVaccinationDateFactory _factory;
        private const int OWNER_ID = 2;
        private const int PET_ID = 2;


        public NextVaccinationDateDomainServiceTests()
        {
            var strategyList = new List<INextVaccinationDateStrategy>()
            {
                new DefaultPetNextVaccinationDateStrategy(),
                new DogSevenWeeksOldStrategy(),
                new DogTenWeeksOldStrategy(),
            };

            IEnumerable<INextVaccinationDateStrategy> strategyEnumerable = strategyList;
            _factory = new NextVaccinationDateFactory(strategyEnumerable);
            _ownerRepository = new Mock<IRepository<Owner>>();
            _sut = new VaccinationDateService(_factory, _ownerRepository.Object );
            _fixture = new Fixture();
            _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }


        [Fact]
        public async Task NextVaccinationDateService_ReturnsOneYear_ForPet_OlderThanTenWeekAsync()
        {
            var correctNextVaccinationDate = DateTime.Now.AddYears(1).Date;
            var ownerWithPet = GetOwnerWithPet(DateTime.Now.AddYears(1));

            _ownerRepository.Setup(r => r.Get(2)).Returns(Task.FromResult(ownerWithPet));
            var result = await _sut.GetNextVaccinationDateForPet(OWNER_ID, PET_ID);

            result.ShouldBeEquivalentTo(correctNextVaccinationDate);
        }
        [Fact]
        public async Task NextVaccinationDateService_ReturnsThreeWeeks_ForTenWeeksOldPetAsync()
        {
            var correctNextVaccinationDate = DateTime.Now.AddDays(3 * 7).Date;

            var ownerWithPet = GetOwnerWithPet(DateTime.Now.AddDays(-(10 * 7)));

            _ownerRepository.Setup(r => r.Get(2)).Returns(Task.FromResult(ownerWithPet));

            var result = await _sut.GetNextVaccinationDateForPet(OWNER_ID, PET_ID);

            result.ShouldBeEquivalentTo(correctNextVaccinationDate);
        }

        [Fact]
        public async Task NextVaccinationDateService_ReturnsThreeWeeks_ForSevenWeeksOldPetAsync()
        {
            var ownerWithPet = GetOwnerWithPet(DateTime.Now.AddDays(-(7 * 7)));
            var correctNextVaccinationDate = DateTime.Now.AddDays(3 * 7).Date;

            _ownerRepository.Setup(r => r.Get(2)).Returns(Task.FromResult(ownerWithPet));

            var result = await _sut.GetNextVaccinationDateForPet(OWNER_ID, PET_ID);

            result.ShouldBeEquivalentTo(correctNextVaccinationDate);

        }

        #region ARRANGE HELPERS
        public Owner GetOwnerWithPet(DateTime birthday)
        {
            var owner = GetOwner();
            var pet = GetPet(birthday, owner.Id);

            var o = owner.GetType().GetField("_pets", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            o.SetValue(owner, new List<Pet>() { pet });

            return owner;

        }
        public Owner GetOwner()
        {
            var owner = new Owner("Rabi Habibi", "Moskovska 21", "Mizirovo", "099939929", "Opstina BlaBla", "060999350834", 0.0m, "", "email@cc.com");
            var o = owner.GetType().GetProperty("PetId", BindingFlags.Instance | BindingFlags.Public);
            o.SetValue(owner, OWNER_ID);

            return owner;
        }

        public List<CheckupItem> GetCheckupItems()
        {
            return new List<CheckupItem>
            {
                new CheckupItem(12, 1, "ml", "IV", 200.00),
                new CheckupItem(13, 2, "ml", "IV", 200.00),
                new CheckupItem(14, 4, "ml", "IV", 200.00)
            };
        }

        public Pet GetPet(DateTime birthday, int ownerId = 2)
        {

           return new Pet(PET_ID, ownerId, 2, 2, "Zaza", birthday, "M", "Opis", "Nema", "12345678901");
        }
        #endregion
    }
}
