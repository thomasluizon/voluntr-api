using AutoMapper;
using Voluntr.Application.Interfaces.Services;
using Voluntr.Crosscutting.Domain.MediatR;

namespace Voluntr.Application.Services
{
    public class CauseServiceApp(
        IMediatorHandler mediator,
        IMapper mapper
    ) : ICauseServiceApp
    {
    }
}
