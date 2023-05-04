using MediatR;

namespace VDMJasminka.Application.Notifications
{
    public class CheckupSuccessfullyCompletedNotification : INotification
    {
        public CheckupSuccessfullyCompletedNotification(string ownerName, string petName)
        {
            OwnerName = ownerName;
            PetName = petName;
        }

        public string OwnerName { get; }
        public string PetName { get; }
    }
}