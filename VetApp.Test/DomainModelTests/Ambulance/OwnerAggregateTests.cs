using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using VDMJasminka.Core.Ambulance.Events;
using VDMJasminka.Core.Ambulance.Exceptions;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Ambulance.Services;
using VDMJasminka.Logic.Constants;
using Xunit;

namespace VDMJasminka.Test.DomainModelTests.Ambulance
{
    public class OwnerAggregateTests
    {
        private readonly Mock<IVaccinationDateService> _vaccinationDateServiceMock;
        private readonly Mock<ICheckupItemInventoryService> _checkupItemInventoryServiceMock;
        private const int PET_ID = 2;
        public OwnerAggregateTests()
        {
            _vaccinationDateServiceMock = new Mock<IVaccinationDateService>();
            _checkupItemInventoryServiceMock = new Mock<ICheckupItemInventoryService>();
        }

        [Fact]
        public void PerformSurgeryOnPet_RasisesEventOfType_PetCheckupSuccessfullyCompleted()
        {
            var owner = GetOwnerWithPet(DateTime.Now.AddYears(-2), false);
            owner.PerformSurgeryOnPet(PET_ID,
                                      DateTime.Now,
                                      "anamneza",
                                      "kl.slika",
                                      "Dijagnoza",
                                      "intervencija",
                                      "lab analiza",
                                      "neka odmara",
                                      DateTime.Now.AddDays(12),
                                      GetCheckupItems(),
                                      299.99,
                                      "penicilin", _checkupItemInventoryServiceMock.Object);

            var @event = owner.DomainEvents.FirstOrDefault() as PetCheckupSuccessfullyCompleted;

            @event.ShouldNotBeNull();
        }

        [Fact]
        public void PerformTreatmentOnPet_RaisesEventOfType_PetCheckupSuccessfullyCompleted()
        {
            var owner = GetOwnerWithPet(DateTime.Now.AddYears(-2), false);
            owner.PerformTreatmentOnPet(PET_ID,
                                        DateTime.Now,
                                        "Anamneza",
                                        "kl slika",
                                        "dijagnoza",
                                        "lab analiza",
                                        "da odmara",
                                        DateTime.Now.AddMonths(1),
                                        GetCheckupItems(),
                                        100.99,
                                        "Nishto", _checkupItemInventoryServiceMock.Object);

            var @event = owner.DomainEvents.FirstOrDefault() as PetCheckupSuccessfullyCompleted;

            @event.ShouldNotBeNull();
        }

        [Fact]
        public async void VaccinatePet_RaisesEventOfType_PetCheckupSuccessfullyCompleted()
        {
            var owner = GetOwnerWithPet(DateTime.Now.AddYears(-2), false);
            var today = DateTime.Today;
            var vaccineType = VaccineType.Rabies;
            var checkupPrice = 399.9;
            var checkupItems = GetCheckupItems();

            // Act
            await owner.VaccinatePet(PET_ID, today, vaccineType, checkupPrice, checkupItems, null, _vaccinationDateServiceMock.Object, _checkupItemInventoryServiceMock.Object);

            var @event = owner.DomainEvents.FirstOrDefault() as PetCheckupSuccessfullyCompleted;

            @event.ShouldNotBeNull();
        }

        [Fact]
        public void ChipPet_RaisesEventOfType_PetCheckupSuccessfullyCompleted()
        {
            var owner = GetOwnerWithPet(DateTime.Now.AddYears(-2), false);
            var today = DateTime.Today;

            owner.ChipPet(PET_ID, today, "12212232123123", 199.9, GetCheckupItems(), _checkupItemInventoryServiceMock.Object);

            var @event = owner.DomainEvents.FirstOrDefault() as PetCheckupSuccessfullyCompleted;
            @event.ShouldNotBeNull();
        }
        [Fact]
        public void RegisterNewPet_RaisesEventOfType_PetRegisteredToOwner()
        {
            var birthday = DateTime.Now.AddDays(-12);
            var owner = GetOwner();
            var pet = GetPet(birthday, false, (int)owner.Id);

            owner.RegisterNewPet(pet.PetInformation.Name,
                                 pet.SpeciesId,
                                 pet.BreedId,
                                 pet.PetInformation.DateOfBirth,
                                 pet.PetInformation.Sex,
                                 pet.PetInformation.Description,
                                 pet.PetInformation.Alergy,
                                 pet.Chip.Number);

            var @event = owner.DomainEvents.FirstOrDefault() as PetRegisteredToOwner;
            @event.ShouldNotBeNull();

        }

        [Fact]
        public void ChipPet_ThrowsPetAlreadyChipped_WhenPetChipIsNotNull()
        {
            var owner = GetOwnerWithPet(DateTime.Now.AddYears(-2), true);
            var chipNo = "16253647362";

            var ex = Record.Exception(() => owner.ChipPet(PET_ID, DateTime.Today, chipNo, 900, GetCheckupItems(), _checkupItemInventoryServiceMock.Object));

            ex.ShouldNotBeNull();
            ex.ShouldBeOfType<PetIsAlreadyChippedException>();
        }

        [Fact]
        public void ChipPet_Throws_ChipNumberIsNotCorrectLengthException_WhenChipNumberIsNotOfCorrectLenght()
        {
            var owner = GetOwnerWithPet(DateTime.Now.AddYears(-2), false);

            var wrongChipNumber = "16272637483736628398172983798712309-898060987163142";

            var ex = Record.Exception(() => owner.ChipPet(PET_ID, DateTime.Today, wrongChipNumber, 900, GetCheckupItems(), _checkupItemInventoryServiceMock.Object));

            ex.ShouldNotBeNull();
            ex.ShouldBeOfType<ChipNumberIsNotCorrectLengthException>();
        }

        [Fact]
        public void GetPet_ThrowsOwnerDoesNotOwnPetException_WhenPetWithProvidedIdDoesNotExist()
        {
            var owner = GetOwnerWithPet(DateTime.Now.AddYears(-2), false);

            var ex = Record.Exception(() => owner.GetPet(PET_ID + 1));

            ex.ShouldNotBeNull();
            ex.ShouldBeOfType<OwnerDoesNotOwnPetException>();

        }

        [Fact]
        public void Archive_RaisesEventTypeOf_OwnerArchived()
        {
            var owner = GetOwnerWithPet(DateTime.Now.AddYears(-2), false);

            owner.Archive();

            var @event = owner.DomainEvents.FirstOrDefault() as OwnerArchived;
            @event.ShouldNotBeNull();
        }

        [Fact]
        public void Restore_RaisesEventTypeOf_OwnerRestoredFromArchive()
        {
            var owner = GetOwnerWithPet(DateTime.Now.AddYears(-2), false);

            owner.Restore();

            var @event = owner.DomainEvents.FirstOrDefault() as OwnerRestoredFromArchive;
            @event.ShouldNotBeNull();
        }

        [Fact]
        public void ChangeEmail_RaisesEventOfType_OwnerEmailChanged()
        {
            var owner = GetOwnerWithPet(DateTime.Now.AddYears(-2), false);

            owner.ChangeEmail("email@test.com");

            var @event = owner.DomainEvents.FirstOrDefault() as OwnerEmailChanged;
            @event.ShouldNotBeNull();

        }

        [Fact]
        public void ChangeLivingAddress_RaisesEventOfType_OwnerLivingAddressChanged()
        {
            var owner = GetOwnerWithPet(DateTime.Now.AddYears(-2), false);

            owner.ChangeLivingAddress("Adresa", "Lokacija", "Berovo");

            var @event = owner.DomainEvents.FirstOrDefault() as OwnerLivingAddressChanged;
            @event.ShouldNotBeNull();

        }
        [Fact]
        public void ChangePetDescriptionAndAlergies_RaisesEventOfType_PetDescriptionAndAlergyChanged()
        {
            var owner = GetOwnerWithPet(DateTime.Now.AddYears(-2), false);

            owner.ChangePetDescriptionAndAlergies(PET_ID, "Alergija", "Deskripcija");

            var @event = owner.DomainEvents.FirstOrDefault() as PetDescriptionAndAlergyChanged;

            @event.ShouldNotBeNull();

        }


        #region ARRANGE HELPERS
        public Owner GetOwnerWithPet(DateTime birthday, bool withChippedPet)
        {
            var owner = GetOwner();
            var pet = GetPet(birthday, withChippedPet, owner.Id);
            
            var o = owner.GetType().GetField("_pets", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            o.SetValue(owner, new List<Pet>() { pet});

            return owner;

        }
        public Owner GetOwner()
        {
            return new Owner("Rabi Habibi", "Moskovska 21", "Mizirovo", "099939929", "Opstina BlaBla", "060999350834", 0.0m, "", "email@cc.com");
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

        public Pet GetPet(DateTime birthday, bool isChipped, int ownerId = 2)
        {
            return isChipped 
                ? new Pet(PET_ID, ownerId, 2, 2, "Zaza", birthday, "M", "Opis", "Nema", "12345678901") 
                : new Pet(PET_ID, ownerId, 2, 2, "Zaza", birthday, "M", "Opis", "Nema", null);
        }
        #endregion
    }
}
