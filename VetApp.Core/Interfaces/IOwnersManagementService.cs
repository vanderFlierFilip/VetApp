using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDMJasminka.Core.Ambulance.OwnerAggregate;

namespace VDMJasminka.Core.Interfaces
{
    public interface IOwnersManagementService
    {
        IEnumerable<Owner> GetPaginatedOwners(int pageNumber, int pageSize);
        Owner GetOwnerWithPets(int id);
        Task DeleteOwner(int ownerId);
        IEnumerable<Species> GetAnimalTypes();
        IEnumerable<Owner> GetDeletedOwners();
    }
}
