using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CashBack.Domain.Entities;

namespace CashBack.Domain.Repository
{
    public interface IOrderRepository
    {
        Task CreateAsync(Order order);
        Task UpdateAsync(Order order);
        Task DeleteAsync(Guid id);
        Task<Order> GetAsync(Guid orderId);
        Task<ICollection<Order>> GetAllAsync(Guid resellerId);
    }
}
