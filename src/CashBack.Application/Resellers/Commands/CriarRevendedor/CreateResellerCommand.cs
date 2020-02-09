using CashBack.Application.Common.Mapping;
using MediatR;

namespace CashBack.Application.Revendedor.Commands.CriarRevendedor
{
    public class CreateResellerCommand : IRequest, IMapTo<Domain.Entities.Reseller>
    {
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
