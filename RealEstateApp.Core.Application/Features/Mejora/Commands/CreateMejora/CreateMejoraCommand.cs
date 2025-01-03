﻿using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Mejora.Commands.CreateMejora
{
    /// <summary>
    /// Parámetros para crear mejoras
    /// </summary>  
    public class CreateMejoraCommand : IRequest<int>
    {
        /// <example>Piscina</example>
        [SwaggerParameter(Description = "El nombre de la mejora")]
        public string Nombre { get; set; }

        /// <example>Una piscina muy grande en el patio</example>
        [SwaggerParameter(Description = "La descripción de la mejora")]
        public string Descripcion { get; set; }
    }
    public class CreateMejoraCommandHandler : IRequestHandler<CreateMejoraCommand, int>
    {
        private readonly IMejoraRepository _mejoraRepository;
        private readonly IMapper _mapper;
        public CreateMejoraCommandHandler(IMejoraRepository mejoraRepository, IMapper mapper)
        {
            _mejoraRepository = mejoraRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateMejoraCommand request, CancellationToken cancellationToken)
        {
            var mejora = _mapper.Map<RealEstateApp.Core.Domain.Entities.Mejora>(request);
            await _mejoraRepository.AddAsync(mejora);
            return mejora.Id;
        }
    }
}