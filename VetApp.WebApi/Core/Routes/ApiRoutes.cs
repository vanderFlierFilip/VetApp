namespace VDMJasminka.WebApi.Core.Routes
{
#pragma warning disable 1591

    public class ApiRoutes
    {
        public const string ROOT = "API";
        public const string VERSION = "v1";
        public const string BASE = ROOT + "/" + VERSION;

        public static class Ambulance
        {
            public const string GetVeterinaryTreatmentViewModel = BASE + "/[controller]/{ownerId}/{petId}";
            public const string GetCheckupTypeMedicalRecord = BASE + "/[controller]/{petId}";
            public const string SaveVeterinaryTreatment = BASE + "/[controller]/";
            public const string GetPetOwners = BASE + "/[controller]/";
            public const string GetOwnerAndPetMedicalRecordInformation = BASE + "/[controller]/{ownerId}/{petId}";
            public const string RegisterNewOwnerWithPets = BASE + "/[controller]/create";
            public const string GetOwner = BASE + "/[controller]/{id}";
        }

        public static class Auth
        {
            public const string Login = BASE + "/[controller]/login";
        }

        public static class Warehouse
        {
            public const string GetSingleItemFromInventory = BASE + "/[controller]/{id}";
            public const string GetInventoryItems = BASE + "/[controller]/";
            public const string PatchMinimalAmountOfItem = BASE + "/[controller]/{id}/change-minimal-amount";
            public const string AddItem = BASE + "/[controller]";
            public const string PatchItemPrice = BASE + "/[controller]/{id}/change-price";
            public const string PatchItemDetails = BASE + "/[controller]/{id}/change-details";
            public const string RecieveItemsFromSupplier = BASE + "/[controller]/entries";
            public const string GetSuppliers = BASE + "/[controller]/";
            public const string GetSingleSupplier = BASE + "/[controller]/{id}";
            public const string RecieveOrder = BASE + "/[controller]/{id}/recieve-order";
        }
    }
}