using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedades;
using RealEstateApp.Core.Domain.Entities;
using System.Linq.Expressions;

namespace RealEstateApp.Core.Application.Interfaces.Repositories
{
    public interface IFavoritoRepository : IGenericRepository<Favorito>
    {
        IQueryable<Favorito> Find(Expression<Func<Favorito, bool>> predicate);
    }
}
