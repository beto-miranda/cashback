using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CashBack.Application.Common.Exceptions;
using CashBack.Application.Common.Security;
using CashBack.Domain.Entities;
using CashBack.Domain.Repository;
using MediatR;

namespace CashBack.Application.Revendedor.Commands.CriarRevendedor
{
    public class CreateResellerCommandHandler : IRequestHandler<CreateResellerCommand>
    {
        private readonly IMapper mapper;
        private readonly IResellerRepository resellerRepository;

        public CreateResellerCommandHandler(IMapper mapper, IResellerRepository resellerRepository)
        {
            this.mapper = mapper;
            this.resellerRepository = resellerRepository;
        }
        public async Task<Unit> Handle(CreateResellerCommand request, CancellationToken cancellationToken)
        {
            var existingReseller = await resellerRepository.GetByCpfAsync(request.CPF);
            if(existingReseller != null)
            {
                throw new AlreadyExistsException(nameof(Reseller), request.CPF);
            }
            var reseller = mapper.Map<Domain.Entities.Reseller>(request);
            reseller.Password = PasswordHasher.HashPassword(reseller.Password);
            reseller.Created = reseller.Updated = DateTime.Now;
            await resellerRepository.CreateAsync(reseller);
            return Unit.Value;
        }
    }
}
