using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CashBack.Domain.Entities;
using CashBack.Application.Orders.Commands.CreateOrder;
using MediatR;
using CashBack.Application.Orders.Commands.EditOrder;
using CashBack.Application.Orders.Commands.DeleteOrder;
using CashBack.Application.Orders.Queries.GetOrders;
using CashBack.Application.Orders.Models;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;

namespace CashBack.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator mediator;

        public OrdersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("{id}")]
        public Task<ActionResult<Order>> GetOrder()
        {
            return null;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetOrders()
        {
            string userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return await mediator.Send(new GetOrdersQuery(Guid.Parse(userId)));
        }

        [HttpPost]
        public async Task<ActionResult<Order>> PostOrder(CreateOrderCommand command)
        {
            var order = await mediator.Send(command);
            return CreatedAtAction(nameof(GetOrder), new { id = order.Id }, order);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, EditOrderCommand command)
        {
            command.OrderId = id;
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            await mediator.Send(new DeleteOrderCommand(id));
            return NoContent();
        }
    }
}
