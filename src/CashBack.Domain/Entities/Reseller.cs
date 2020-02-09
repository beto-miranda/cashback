using System;
using System.Collections.Generic;

namespace CashBack.Domain.Entities
{
    public class Reseller
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Order> Orders { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
    }
}
