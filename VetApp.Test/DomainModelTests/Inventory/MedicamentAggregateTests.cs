using Shouldly;
using System;
using System.Collections.Generic;
using System.Text;
using VDMJasminka.Core.Inventory.MedicamentAggregate;
using VDMJasminka.Core.Inventory.ValueObjects;
using Xunit;

namespace VDMJasminka.Test.DomainModelTests.Inventory
{
    public class MedicamentAggregateTests
    {
        private List<InventoryAdjustment> _inventoryAdjustments = new List<InventoryAdjustment>();

        [Fact]
        public void Constructor_Should_Set_Properties_Correctly()
        {
            var medicament = new Medicament("name", "uom", 10, 5, true);

            medicament.MedicamentDetails.Name.ShouldBe("name");
            medicament.MedicamentDetails.Uom.ShouldBe("uom");
            medicament.MedicamentDetails.IsMaterial.ShouldBe(true);
            medicament.MinimalAmount.Value.ShouldBe(5);
            medicament.Price.Value.ShouldBe(10);
        }

        [Theory]
        [InlineData(20, true)]
        [InlineData(10, false)]
        public void ChangePrice_Should_Change_Price_Correctly(double amount, bool expectedResult)
        {
            var medicament = new Medicament("name", "uom", 10, 5, true);

            var result = medicament.ChangePrice(amount);

            result.ShouldBe(expectedResult);
            medicament.Price.Value.ShouldBe(amount);
        }

        [Fact]
        public void AdjustAmountInInventory_Should_Add_InventoryAdjustment_Correctly()
        {
            var medicament = new Medicament("name", "uom", 10, 5, true);
            var adjustment = new InventoryAdjustment(medicament.Id, 5, new InventoryAdjustmentReason("reason"));

            medicament.AdjustAmountInInventory(5, "reason");

            medicament.InventoryAdjustments.Count.ShouldBe(1);
            medicament.InventoryAdjustments[0].ShouldBe(adjustment);
        }

        [Theory]
        [InlineData(20, true)]
        [InlineData(5, false)]
        public void ChangeMinimalAmountInStock_Should_Change_MinimalAmount_Correctly(int amount, bool expectedResult)
        {
            var medicament = new Medicament("name", "uom", 10, 5, true);

            medicament.ChangeMinimalAmountInStock(amount);

            medicament.MinimalAmount.Value.ShouldBe(amount);
        }

        [Fact]
        public void IsInStock_Should_Return_Correct_Result()
        {
            //var medicament = new Medicament("name", "uom", 10, 5, true);
            //var service = new MedicamentInventoryServiceStub();

            //var result = medicament.IsInStock(service);

            //result.ShouldBe(service.IsMedicamentInStock(medicament.PetId));
        }

        [Fact]
        public void GetMinimalAmount_Should_Return_MinimalAmount_Value()
        {
            var medicament = new Medicament("name", "uom", 10, 5, true);

            var result = medicament.GetMinimalAmount();

            result.ShouldBe((short)medicament.MinimalAmount.Value);
        }
    }
}
