using System;
using System.Threading.Tasks;
using CashBack.Domain.Entities;

namespace CashBack.Domain.Repository
{
    public interface IResellerRepository
    {
        Task CreateAsync(Reseller reseller);
        Task<Reseller> GetByCpfAsync(string cpf);
        Task<Reseller> GetByEmail(string email);
        Task<Reseller> GetByIdAsync(Guid id);
    }
}
