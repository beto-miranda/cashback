using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using CashBack.Application.Resellers.Queries.GetAccumulatedCashback;
using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Security.Claims;
using System;

namespace CashBack.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CreditController : ControllerBase
    {
        private readonly IMediator mediator;

        public CreditController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<GetCreditsResult>> Get(string id)
        {
            string userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            return await mediator.Send(new GetCreditsQuery(Guid.Parse(userId)));
        }
    }
}
