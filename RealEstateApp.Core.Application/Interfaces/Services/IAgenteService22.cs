using RealEstateApp.Core.Application.ViewModels.Agentes;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAgenteService22
    {
        Task<List<AgenteViewModel>> GetAllViewModelWithInclude();
    }
}