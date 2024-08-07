using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.ViewModels.Mejoras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Mejora.Queries.GetAllMejoras
{
    /// <summary>
    /// Parámetros para el listado de mejoras
    /// </summary>  
    public class GetAllMejorasQuery : IRequest<IEnumerable<MejoraViewModel>>
    {
    }
    public class GetAllMejorasQueryHandler : IRequestHandler<GetAllMejorasQuery, IEnumerable<MejoraViewModel>>
    {
        private readonly IMejoraRepository _mejoraRepository;
        private readonly IMapper _mapper;
        public GetAllMejorasQueryHandler(IMejoraRepository mejoraRepository, IMapper mapper)
        {
            _mejoraRepository = mejoraRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MejoraViewModel>> Handle(GetAllMejorasQuery request, CancellationToken cancellationToken)
        {
            var mejorasViewModel = await GetAllViewModelWithInclude();
            return mejorasViewModel;
        }

        private async Task<List<MejoraViewModel>> GetAllViewModelWithInclude()
        {
            var mejoraList = await _mejoraRepository.GetAllAsync();

            return mejoraList.Select(mejora => new MejoraViewModel
            {
                Id = mejora.Id,
                Nombre = mejora.Nombre,
                Descripcion = mejora.Descripcion
            }).ToList();
        }
    }
}
