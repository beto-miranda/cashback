using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CashBack.Data.EntityFramework;
using CashBack.Domain.Entities;
using MediatR;
using CashBack.Application.Revendedor.Commands.CriarRevendedor;
using CashBack.Application.Resellers.Queries.GetAccumulatedCashback;

namespace CashBack.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResellerController : ControllerBase
    {
        private readonly IMediator mediator;

        public ResellerController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Reseller>> PostReseller(CreateResellerCommand command)
        {
            await mediator.Send(command);
            return CreatedAtAction("GetReseller", new { id = "123" }, command);
        }
    }
}
