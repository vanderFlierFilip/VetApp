using System.Collections.Generic;
using System.Threading.Tasks;
using VDMJasminka.Core.Ambulance.OwnerAggregate;

namespace VDMJasminka.Core.Interfaces
{
    public interface ICheckupRepository
    {
        IEnumerable<Checkup> GetAll();
        Checkup Get(int id);
        Task<Checkup> Add(Checkup entity);
        Task Update(Checkup entity);
        Task Delete(Checkup entity);
    }
}
