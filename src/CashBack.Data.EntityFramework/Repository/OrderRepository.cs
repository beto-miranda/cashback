using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CashBack.Domain.Entities;
using CashBack.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace CashBack.Data.EntityFramework.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Task CreateAsync(Order order)
        {
            dbContext.Orders.Add(order);
            return dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await dbContext.Orders.FindAsync(id);
            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync();
        }

        public async Task<ICollection<Order>> GetAllAsync(Guid resellerId)
        {
            return await dbContext.Orders.Where(o => o.Reseller.Id == resellerId).ToListAsync();
        }

        public Task<Order> GetAsync(Guid orderId)
        {
            return dbContext.Orders.FindAsync(orderId).AsTask();
        }

        public async Task UpdateAsync(Order order)
        {
            dbContext.Entry(order).State = EntityState.Modified;
            await dbContext.SaveChangesAsync();
        }
    }
}