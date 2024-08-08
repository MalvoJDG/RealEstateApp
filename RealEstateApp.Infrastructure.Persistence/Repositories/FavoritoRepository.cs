using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;
using System.Linq.Expressions;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class FavoritoRepository : GenericRepository<Favorito>, IFavoritoRepository
    {
        private readonly ApplicationContext _dbContext;

        public FavoritoRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Favorito> Find(Expression<Func<Favorito, bool>> predicate)
        {
            return _dbContext.Set<Favorito>().Where(predicate);
        }

    }
}

