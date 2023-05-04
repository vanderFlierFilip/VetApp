using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDMJasminka.Core;
using VDMJasminka.Core.Interfaces;

namespace VDMJasminka.Data.Repository
{
    public class SpeciesRepository : IRepository<Species>
    {
        private readonly VDMJasminkaDbContext _dbContext;

        public SpeciesRepository(VDMJasminkaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Species> Add(Species entity)
        {
            _dbContext.BeginTransaction();
            _dbContext.AnimalTypes.Add(entity);
            await _dbContext.CommitAsync();
            return entity;
        }

        public async Task Delete(Species entity)
        {
            _dbContext.AnimalTypes.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Species> Get(int id)
        {
            return await _dbContext.AnimalTypes.FindAsync(id);
        }

        public async Task<IQueryable<Species>> GetAll()
        {
            return await Task.FromResult(_dbContext.AnimalTypes
                .Include(a => a.Breeds));
        }

        public async Task Update(Species entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

    }
}
