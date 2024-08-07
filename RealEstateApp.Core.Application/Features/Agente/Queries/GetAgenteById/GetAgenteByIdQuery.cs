using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Agentes;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Agente.Queries.GetAgenteById
{
    /// <summary>
    /// Parámetros para buscar al agente por id
    /// </summary>  
    public class GetAgenteByIdQuery : IRequest<AgenteViewModel>
    {
        /// <example>f136c302-bf1d-44a1-8555-7d8ae497cce2</example>
        [SwaggerParameter(Description = "El id del agente")]
        public string Id { get; set; }
    }

    public class GetAgenteByIdQueryHandler : IRequestHandler<GetAgenteByIdQuery, AgenteViewModel>
    {
        private readonly IAgenteService _agenteService;
        private readonly IMapper _mapper;

        public GetAgenteByIdQueryHandler(IAgenteService agenteService, IMapper mapper)
        {
            _agenteService = agenteService;
            _mapper = mapper;
        }

        public async Task<AgenteViewModel> Handle(GetAgenteByIdQuery query, CancellationToken cancellationToken)
        {
            var agente = await _agenteService.GetAgenteByIdAsync(query.Id);
            if (agente == null) throw new Exception($"Agente no encontrado");

            var agenteVm = _mapper.Map<AgenteViewModel>(agente);
            return agenteVm;
        }
    }
}