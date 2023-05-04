// Code scaffolded by EF Core assumes nullable reference types ColumnNRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Ambulance.Events
{
    public class OwnerEmailChanged : IDomainEvent
    {
        public OwnerEmailChanged(string email)
        {
            Email = email;
        }

        public string Email { get; }
    }
}