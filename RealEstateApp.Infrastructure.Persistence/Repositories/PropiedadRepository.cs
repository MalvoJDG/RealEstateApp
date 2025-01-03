﻿using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class PropiedadRepository : GenericRepository<Propiedad>, IPropiedadRepository
    {
        private readonly ApplicationContext _dbContext;

        public PropiedadRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> CountByAgenteIdAsync(string agenteId)
        {
            return await _dbContext.Propiedades.CountAsync(p => p.AgenteId == agenteId);
        }

        public async Task<List<Propiedad>> GetAllWithFavoritesAsync()
        {
            return await _dbContext.Propiedades
                .Include(p => p.Favorito)
                .ToListAsync();
        }

    }
}

