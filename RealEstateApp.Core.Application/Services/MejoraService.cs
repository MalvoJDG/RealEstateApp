using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Mejoras;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Services
{
    public class MejoraService : GenericService<SaveMejoraViewModel, MejoraViewModel, Mejora>, IMejoraService
    {
        private readonly IMejoraRepository _mejoraRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public MejoraService(IMejoraRepository mejoraRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(mejoraRepository, mapper)
        {
            _mejoraRepository = mejoraRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }
    }
}
