// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Common;

namespace VDMJasminka.Core.Ambulance.Events
{
    public class CheckupItemOutOfStock : IDomainEvent
    {
        public CheckupItemOutOfStock(CheckupItem checkupItem)
        {
            CheckupItem = checkupItem;
        }

        public CheckupItem CheckupItem { get; }
    }
}