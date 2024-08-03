using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Mejora.Commands.UpdateMejora
{
    public class UpdateMejoraCommand : IRequest<MejoraUpdateResponse>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
    }
    public class UpdateMejoraCommandHandler : IRequestHandler<UpdateMejoraCommand, MejoraUpdateResponse>
    {
        private readonly IMejoraRepository _mejoraRepository;
        private readonly IMapper _mapper;

        public UpdateMejoraCommandHandler(IMejoraRepository mejoraRepository, IMapper mapper)
        {
            _mejoraRepository = mejoraRepository;
            _mapper = mapper;
        }
        public async Task<MejoraUpdateResponse> Handle(UpdateMejoraCommand command, CancellationToken cancellationToken)
        {
            var mejora = await _mejoraRepository.GetByIdAsync(command.Id);

            if (mejora == null)
            {
                throw new Exception($"Mejora no encontrada");
            }
            else
            {
                mejora = _mapper.Map<RealEstateApp.Core.Domain.Entities.Mejora>(command);
                await _mejoraRepository.UpdateAsync(mejora, mejora.Id);
                var categoryVm = _mapper.Map<MejoraUpdateResponse>(mejora);

                return categoryVm;
            }
        }
    }
}