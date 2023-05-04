using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Core.Entities;
using VDMJasminka.Core.Interfaces;

namespace VDMJasminka.Data.Repository
{
    public class InventoryRepository : IReadOnlyRepository<MedicamentInventory>
    {
        private readonly VDMJasminkaDbContext _context;

        public InventoryRepository(VDMJasminkaDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<MedicamentInventory> GetAsync(int id)
        {
            return await _context.Inventory
                .FirstOrDefaultAsync(x => x.MedicamentId == id);
        }


        public async Task<IEnumerable<MedicamentInventory>> GetAllAsync() {
            return await _context.Inventory.ToListAsync();
        }

        public MedicamentInventory Get(int id)
        {
            // query (Npgslq) can't reuse same connection, have to initialize dbContext like this.
            // TODO: Find a better way
            var ctx = new VDMJasminkaDbContext(); 
            return ctx.Inventory.FirstOrDefault(o => o.MedicamentId == id);
        }
    }
}
