using RealEstateApp.Core.Application.ViewModels.Propiedades;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IPropiedadService : IGenericService<SavePropiedadViewModel, PropiedadViewModel, Propiedad>
    {
        Task<int> GetPropiedadesCountByAgenteId(string agenteId);
        Task<IEnumerable<PropiedadDto>> GetPropiedadesByAgenteId(string agenteId);
        Task<List<PropiedadViewModel>> GetAllFavoriteProperties(string userId);
        Task<PropiedadViewModel> GetByIdViewModel(int id);
        Task<List<PropiedadViewModel>> GetAllViewModelWithFilters(FilterPropiedadViewModel filters);
        Task<List<PropiedadViewModel>> GetAllByAgente(string agenteId);
    }
}
