using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Inventory.MedicamentAggregate;

namespace VDMJasminka.Data.Repository
{
    public class MedicamentRepository : IRepository<Medicament>
    {
        private readonly VDMJasminkaDbContext _context;

        public MedicamentRepository(VDMJasminkaDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Medicament>> GetAll()
        {
            return await Task.FromResult(_context.Medicaments);
        }
        public async Task<Medicament> Get(int id)
        {
            return await _context.Medicaments.FindAsync(id);
        }

        public async Task<Medicament> Add(Medicament entity)
        {
            _context.BeginTransaction();
            _context.Medicaments.Add(entity);
            await _context.CommitAsync();
            return entity;
        }

        public async Task Delete(Medicament entity)
        {
            _context.BeginTransaction();
            _context.Medicaments.Remove(entity);
            await _context.CommitAsync();

        }


        public async Task Update(Medicament entity)
        {
            _context.BeginTransaction();
            _context.Entry(entity).State = EntityState.Modified;
            await _context.CommitAsync();
        }
    }
}