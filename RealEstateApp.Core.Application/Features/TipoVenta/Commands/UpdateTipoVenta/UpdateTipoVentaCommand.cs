using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.TipoVenta.Commands.UpdateTipoVenta
{
    /// <summary>
    /// Parámetros para modificar un tipo de venta
    /// </summary>  
    public class UpdateTipoVentaCommand : IRequest<TipoVentaUpdateResponse>
    {
        /// <example>1</example>
        [SwaggerParameter(Description = "El id del tipo de venta")]
        public int Id { get; set; }

        /// <example>Alquilada</example>
        [SwaggerParameter(Description = "El nombre del tipo de venta")]
        public string Nombre { get; set; }

        /// <example>No permitido mascotas</example>
        [SwaggerParameter(Description = "La descripcion del tipo de venta")]
        public string Descripcion { get; set; }
    }
    public class UpdateTipoVentaCommandHandler : IRequestHandler<UpdateTipoVentaCommand, TipoVentaUpdateResponse>
    {
        private readonly ITipoVentaRepository _tipoVentaRepository;
        private readonly IMapper _mapper;

        public UpdateTipoVentaCommandHandler(ITipoVentaRepository tipoVentaRepository, IMapper mapper)
        {
            _tipoVentaRepository = tipoVentaRepository;
            _mapper = mapper;
        }
        public async Task<TipoVentaUpdateResponse> Handle(UpdateTipoVentaCommand command, CancellationToken cancellationToken)
        {
            var tipoVenta = await _tipoVentaRepository.GetByIdAsync(command.Id);

            if (tipoVenta == null)
            {
                throw new Exception($"TipoVenta no encontrada");
            }
            else
            {
                tipoVenta = _mapper.Map<RealEstateApp.Core.Domain.Entities.TipoVenta>(command);
                await _tipoVentaRepository.UpdateAsync(tipoVenta, tipoVenta.Id);
                var categoryVm = _mapper.Map<TipoVentaUpdateResponse>(tipoVenta);

                return categoryVm;
            }
        }
    }
}
