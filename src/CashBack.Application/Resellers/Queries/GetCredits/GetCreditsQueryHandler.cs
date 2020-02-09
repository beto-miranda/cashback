using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CashBack.Application.Common.Exceptions;
using CashBack.Application.Infrastructure.HttpClients;
using CashBack.Domain.Entities;
using CashBack.Domain.Repository;
using MediatR;

namespace CashBack.Application.Resellers.Queries.GetAccumulatedCashback
{
    public class GetCreditsQueryHandler : IRequestHandler<GetCreditsQuery, GetCreditsResult>
    {
        private readonly BoticarioHttpClient httpClient;
        private readonly IResellerRepository resellerRepository;

        public GetCreditsQueryHandler(BoticarioHttpClient httpClient, IResellerRepository resellerRepository)
        {
            this.httpClient = httpClient;
            this.resellerRepository = resellerRepository;
        }

        public async Task<GetCreditsResult> Handle(GetCreditsQuery request, CancellationToken cancellationToken)
        {
            var reseller = await resellerRepository.GetByIdAsync(request.ResellerId);

            if (reseller == null)
            {
                throw new NotFoundException(nameof(Reseller), request.ResellerId.ToString());
            }

            var response = await httpClient.Instance.GetAsync($"v1/cashback?cpf={reseller.CPF}");
            response.EnsureSuccessStatusCode();
            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                var result = await JsonSerializer.DeserializeAsync<BoticationApiResponse>(responseStream);
                return new GetCreditsResult(result.body.credit);
            }
        }

        class Body
        {
            public int credit { get; set; }
        }

        class BoticationApiResponse
        {
            public int statusCode { get; set; }
            public Body body { get; set; }
        }
    }
}