using MediatR;
using RealEstateApp.Core.Application.Interfaces.Services;

namespace RealEstateApp.Core.Application.Features.Agente.Commands.ChangeEmailConfirmedStatus
{
    public class ChangeEmailConfirmedStatusCommand : IRequest<bool>
    {
        public string Id { get; set; }
        public bool IsConfirmed { get; set; }
    }
    public class ChangeEmailConfirmedStatusCommandHandler : IRequestHandler<ChangeEmailConfirmedStatusCommand, bool>
    {
        private readonly IAgenteService _agenteService;

        public ChangeEmailConfirmedStatusCommandHandler(IAgenteService agenteService)
        {
            _agenteService = agenteService;
        }

        public async Task<bool> Handle(ChangeEmailConfirmedStatusCommand request, CancellationToken cancellationToken)
        {
            var result = await _agenteService.ChangeEmailConfirmedStatusAsync(request.Id, request.IsConfirmed);
            if (!result) throw new Exception("No se pudo cambiar el estado. Asegúrese de que el ID corresponde a un agente.");

            return result;
        }
    }
}
