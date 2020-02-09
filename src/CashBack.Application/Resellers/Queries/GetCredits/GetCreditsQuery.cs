using System;
using MediatR;

namespace CashBack.Application.Resellers.Queries.GetAccumulatedCashback
{
    public class GetCreditsQuery : IRequest<GetCreditsResult>
    {
        public GetCreditsQuery(Guid resellerId)
        {
            ResellerId = resellerId;
        }

        public Guid ResellerId { get; set; }
    }
}