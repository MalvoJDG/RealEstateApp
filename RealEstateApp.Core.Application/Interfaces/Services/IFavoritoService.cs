using RealEstateApp.Core.Application.ViewModels.Favoritos;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedades;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IFavoritoService : IGenericService<SaveFavoritosViewModel, FavoritosViewModel, Favorito>
    {
        Task AddFavorite(string userId, int propiedadId);
        Task<bool> IsFavorite(string userId, int propiedadId);
        Task RemoveFavorite(string userId, int propiedadId);
    }
}
