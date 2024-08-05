using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.TipoPropiedad.Commands.DeleteTipoPropiedadById
{
    /// <summary>
    /// Parámetros para eliminar un tipo de propiedad
    /// </summary>  
    public class DeleteTipoPropiedadByIdCommand : IRequest<int>
    {
        /// <example>1</example>
        [SwaggerParameter(Description = "El id del tipo de propiedad")]
        public int Id { get; set; }
    }
    public class DeleteTipoPropiedadByIdCommandHandler : IRequestHandler<DeleteTipoPropiedadByIdCommand, int>
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        public DeleteTipoPropiedadByIdCommandHandler(ITipoPropiedadRepository tipoPropiedadRepository)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
        }
        public async Task<int> Handle(DeleteTipoPropiedadByIdCommand command, CancellationToken cancellationToken)
        {
            var tipoPropiedad = await _tipoPropiedadRepository.GetByIdAsync(command.Id);
            if (tipoPropiedad == null) throw new Exception($"TipoPropiedad no encontrada");
            await _tipoPropiedadRepository.DeleteAsync(tipoPropiedad);
            return tipoPropiedad.Id;
        }
    }
}