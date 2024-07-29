using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.TipoVenta.Commands.DeleteTipoVentaById
{
    public class DeleteTipoVentaByIdCommand : IRequest<int>
    {
        public int Id { get; set; }
    }
    public class DeleteTipoVentaByIdCommandHandler : IRequestHandler<DeleteTipoVentaByIdCommand, int>
    {
        private readonly ITipoVentaRepository _tipoVentaRepository;
        public DeleteTipoVentaByIdCommandHandler(ITipoVentaRepository tipoVentaRepository)
        {
            _tipoVentaRepository = tipoVentaRepository;
        }
        public async Task<int> Handle(DeleteTipoVentaByIdCommand command, CancellationToken cancellationToken)
        {
            var tipoVenta = await _tipoVentaRepository.GetByIdAsync(command.Id);
            if (tipoVenta == null) throw new Exception($"TipoVenta no encontrada");
            await _tipoVentaRepository.DeleteAsync(tipoVenta);
            return tipoVenta.Id;
        }
    }
}