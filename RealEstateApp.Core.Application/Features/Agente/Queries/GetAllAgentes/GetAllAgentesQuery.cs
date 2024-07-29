using MediatR;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Agentes;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Agente.Queries.GetAllAgentes
{
    public class GetAllAgentesQuery : IRequest<IEnumerable<AgenteViewModel>>
    {
    }

    public class GetAllAgentesQueryHandler : IRequestHandler<GetAllAgentesQuery, IEnumerable<AgenteViewModel>>
    {
        private readonly IAgenteService _agenteService;

        public GetAllAgentesQueryHandler(IAgenteService agenteService)
        {
            _agenteService = agenteService;
        }

        public async Task<IEnumerable<AgenteViewModel>> Handle(GetAllAgentesQuery request, CancellationToken cancellationToken)
        {
            return await _agenteService.GetAgentesAsync();
        }
    }
}