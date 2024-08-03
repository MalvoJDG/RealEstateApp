using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.TipoPropiedad.Commands.UpdateTipoPropiedad
{
    public class UpdateTipoPropiedadCommand : IRequest<TipoPropiedadUpdateResponse>
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
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