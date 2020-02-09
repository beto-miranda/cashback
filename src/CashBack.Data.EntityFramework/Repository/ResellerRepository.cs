using System;
using System.Threading.Tasks;
using CashBack.Domain.Entities;
using CashBack.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace CashBack.Data.EntityFramework.Repository
{
    public class ResellerRepository : IResellerRepository
    {
        private readonly ApplicationDbContext dbContext;

        public ResellerRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(Reseller revendedor)
        {
            dbContext.Resellers.Add(revendedor);
            await dbContext.SaveChangesAsync();
        }

        public Task<Reseller> GetByCpfAsync(string cpf)
        {
            return dbContext.Resellers.SingleOrDefaultAsync(r => r.CPF == cpf);
        }

        public Task<Reseller> GetByEmail(string email)
        {
            return dbContext.Resellers.SingleOrDefaultAsync(r => r.Email == email);
        }

        public Task<Reseller> GetByIdAsync(Guid id)
        {
            return dbContext.Resellers.SingleOrDefaultAsync(r => r.Id == id);
        }
    }
}
