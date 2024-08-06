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

namespace RealEstateApp.Core.Application.Features.TipoPropiedad.Commands.UpdateTipoPropiedad
{
    /// <summary>
    /// Parámetros para modificar un tipo de propiedad
    /// </summary>  
    public class UpdateTipoPropiedadCommand : IRequest<TipoPropiedadUpdateResponse>
    {
        /// <example>1</example>
        [SwaggerParameter(Description = "El id del tipo de propiedad")]
        public int Id { get; set; }

        /// <example>Apartamento</example>
        [SwaggerParameter(Description = "El nombre del tipo de propiedad")]
        public string Nombre { get; set; }

        /// <example>Un apartamento con vista al mar</example>
        [SwaggerParameter(Description = "La descripcion del tipo de propiedad")]
        public string Descripcion { get; set; }
    }
    public class UpdateTipoPropiedadCommandHandler : IRequestHandler<UpdateTipoPropiedadCommand, TipoPropiedadUpdateResponse>
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        private readonly IMapper _mapper;

        public UpdateTipoPropiedadCommandHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
            _mapper = mapper;
        }
        public async Task<TipoPropiedadUpdateResponse> Handle(UpdateTipoPropiedadCommand command, CancellationToken cancellationToken)
        {
            var tipoPropiedad = await _tipoPropiedadRepository.GetByIdAsync(command.Id);

            if (tipoPropiedad == null)
            {
                throw new Exception($"TipoPropiedad no encontrada");
            }
            else
            {
                tipoPropiedad = _mapper.Map<RealEstateApp.Core.Domain.Entities.TipoPropiedad>(command);
                await _tipoPropiedadRepository.UpdateAsync(tipoPropiedad, tipoPropiedad.Id);
                var categoryVm = _mapper.Map<TipoPropiedadUpdateResponse>(tipoPropiedad);

                return categoryVm;
            }
        }
    }
}