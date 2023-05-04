using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Core.Ambulance.OwnerAggregate;
using VDMJasminka.Core.Interfaces;

namespace VDMJasminka.Data.Repository
{
    public class OwnerRepository : IRepository<Owner>
    {
        private VDMJasminkaDbContext _context;

        public OwnerRepository(VDMJasminkaDbContext context)
        {
            _context = context;
        }

        public async Task<Owner> Get(int id)
        {
            return await _context.Owners.Include(o => o.Pets)
                .ThenInclude(p => p.Breed)
                .ThenInclude(p => p.Species)
                .Include(x => x.Pets).ThenInclude(p => p.Checkups).ThenInclude(x => x.CheckupItems)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IQueryable<Owner>> GetAll()
        {
            return await Task.FromResult(_context.Owners
                .Include(o => o.Pets)
                .ThenInclude(p => p.Breed)
                .ThenInclude(p => p.Species)
                .Include(x => x.Pets).ThenInclude(x => x.Checkups).ThenInclude(x => x.CheckupItems));
        }

        public async Task Delete(Owner entity)
        {
            _context.Owners.Remove(entity);
            await _context.CommitAsync();
        }

        public async Task Update(Owner entity)
        {
            _context.BeginTransaction();
            _context.Entry(entity).State = EntityState.Modified;
            await _context.CommitAsync();
        }

        public async Task<Owner> Add(Owner entity)
        {
            _context.BeginTransaction();
            _context.Owners.Add(entity);
            await _context.CommitAsync();
            return entity;
        }
    }
}