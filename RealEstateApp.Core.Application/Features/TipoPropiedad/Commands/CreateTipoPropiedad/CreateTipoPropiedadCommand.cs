using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.TipoPropiedad.Commands.CreateTipoPropiedad
{
    /// <summary>
    /// Parámetros para crear un tipo de propiedad
    /// </summary>  
    public class CreateTipoPropiedadCommand : IRequest<int>
    {
        /// <example>Apartamento</example>
        [SwaggerParameter(Description = "El nombre del tipo de propiedad")]
        public string Nombre { get; set; }

        /// <example>Un apartamento con vista al mar</example>
        [SwaggerParameter(Description = "La descripcion del tipo de propiedad")]
        public string Descripcion { get; set; }
    }
    public class CreateTipoPropiedadCommandHandler : IRequestHandler<CreateTipoPropiedadCommand, int>
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        private readonly IMapper _mapper;
        public CreateTipoPropiedadCommandHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTipoPropiedadCommand request, CancellationToken cancellationToken)
        {
            var tipoPropiedad = _mapper.Map<RealEstateApp.Core.Domain.Entities.TipoPropiedad>(request);
            await _tipoPropiedadRepository.AddAsync(tipoPropiedad);
            return tipoPropiedad.Id;
        }
    }
}
