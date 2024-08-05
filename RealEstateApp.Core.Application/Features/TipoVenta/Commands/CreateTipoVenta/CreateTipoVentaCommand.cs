using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.TipoVenta.Commands.CreateTipoVenta
{
    /// <summary>
    /// Parámetros para crear un tipo de venta
    /// </summary>  
    public class CreateTipoVentaCommand : IRequest<int>
    {
        /// <example>Alquilada</example>
        [SwaggerParameter(Description = "El nombre del tipo de venta")]
        public string Nombre { get; set; }

        /// <example>No permitido mascotas</example>
        [SwaggerParameter(Description = "La descripcion del tipo de venta")]
        public string Descripcion { get; set; }
    }
    public class CreateTipoVentaCommandHandler : IRequestHandler<CreateTipoVentaCommand, int>
    {
        private readonly ITipoVentaRepository _tipoVentaRepository;
        private readonly IMapper _mapper;
        public CreateTipoVentaCommandHandler(ITipoVentaRepository tipoVentaRepository, IMapper mapper)
        {
            _tipoVentaRepository = tipoVentaRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTipoVentaCommand request, CancellationToken cancellationToken)
        {
            var tipoVenta = _mapper.Map<RealEstateApp.Core.Domain.Entities.TipoVenta>(request);
            await _tipoVentaRepository.AddAsync(tipoVenta);
            return tipoVenta.Id;
        }
    }
}