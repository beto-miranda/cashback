using System;

namespace CashBack.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public Decimal Amount { get; set; }
        public Reseller Reseller { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public decimal? CashbackPercentage { get; set; }
        public decimal? Cashback { get; set; }

        public void ApplyCashback()
        {
            if(Amount <= 1000)
            {
                CashbackPercentage = 0.1m;
            }
            else if (Amount <= 1500)
            {
                CashbackPercentage = 0.15m;
            }
            else
            {
                CashbackPercentage = 0.2m;
            }
            this.Cashback = Amount * CashbackPercentage;
            this.Status = OrderStatus.Approved;
        }
    }

    public enum OrderStatus
    {
        Validation,
        Approved
    }
}
