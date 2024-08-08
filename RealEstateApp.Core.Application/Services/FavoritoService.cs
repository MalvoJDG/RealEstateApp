using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Favoritos;
using RealEstateApp.Core.Domain.Entities;


namespace RealEstateApp.Core.Application.Services
{
    public class FavoritoService : GenericService<SaveFavoritosViewModel, FavoritosViewModel, Favorito>, IFavoritoService
    {
        private readonly IFavoritoRepository _favoritoRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public FavoritoService(IFavoritoRepository favoritoRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(favoritoRepository, mapper)
        {
            _favoritoRepository = favoritoRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task AddFavorite(string userId, int propiedadId)
        {
            var favorito = new Favorito
            {
                User_Id = userId,
                Propiedad_Id = propiedadId
            };

            await _favoritoRepository.AddAsync(favorito);
        }

        public async Task RemoveFavorite(string userId, int propiedadId)
        {
            var favorito = await _favoritoRepository
                .Find(f => f.User_Id == userId && f.Propiedad_Id == propiedadId)
                .FirstOrDefaultAsync();

            if (favorito != null)
            {
                await _favoritoRepository.DeleteAsync(favorito);
            }
        }

        public async Task<bool> IsFavorite(string userId, int propiedadId)
        {
            var favorito = await _favoritoRepository
                .Find(f => f.User_Id == userId && f.Propiedad_Id == propiedadId)
                .FirstOrDefaultAsync();
            return favorito != null;
        }
    }
}
