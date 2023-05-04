using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Core.Interfaces;
using VDMJasminka.Core.Inventory.SupplierAggregate;

namespace VDMJasminka.Data.Repository
{
    public class SupplierRepository : IRepository<Supplier>
    {
        private readonly VDMJasminkaDbContext _context;
        public SupplierRepository(VDMJasminkaDbContext context)
        {
            _context = context;
        }
        public async Task<Supplier> Add(Supplier entity)
        {
            _context.BeginTransaction();
            await _context.AddAsync(entity);
            await _context.CommitAsync();
            return entity;
        }

        public async Task Delete(Supplier entity)
        {
            _context.Suppliers.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<Supplier> Get(int id)
        {
            return await _context.Suppliers.FindAsync(id);
        }

        public async Task<IQueryable<Supplier>> GetAll()
        {
            return await Task.FromResult(_context.Suppliers.Include(x => x.OrderRecievments));
        }

        public async Task Update(Supplier entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}