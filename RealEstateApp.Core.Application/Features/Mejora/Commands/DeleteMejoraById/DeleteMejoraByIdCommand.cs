﻿using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Mejora.Commands.DeleteMejoraById
{
    /// <summary>
    /// Parámetros para eliminar mejoras
    /// </summary>  
    public class DeleteMejoraByIdCommand : IRequest<int>
    {
        /// <example>1</example>
        [SwaggerParameter(Description = "El id de la mejora")]
        public int Id { get; set; }
    }
    public class DeleteMejoraByIdCommandHandler : IRequestHandler<DeleteMejoraByIdCommand, int>
    {
        private readonly IMejoraRepository _mejoraRepository;
        public DeleteMejoraByIdCommandHandler(IMejoraRepository mejoraRepository)
        {
            _mejoraRepository = mejoraRepository;
        }
        public async Task<int> Handle(DeleteMejoraByIdCommand command, CancellationToken cancellationToken)
        {
            var mejora = await _mejoraRepository.GetByIdAsync(command.Id);
            if (mejora == null) throw new Exception($"Mejora no encontrada");
            await _mejoraRepository.DeleteAsync(mejora);
            return mejora.Id;
        }
    }
}
