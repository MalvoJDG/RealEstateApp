using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.ViewModels.Mejoras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Mejora.Queries.GetMejoraById
{
    public class GetMejoraByIdQuery : IRequest<MejoraViewModel>
    {
        public int Id { get; set; }
    }
    public class GetMejoraByIdQueryHandler : IRequestHandler<GetMejoraByIdQuery, MejoraViewModel>
    {
        private readonly IMejoraRepository _mejoraRepository;
        private readonly IMapper _mapper;

        public GetMejoraByIdQueryHandler(IMejoraRepository mejoraRepository, IMapper mapper)
        {
            _mejoraRepository = mejoraRepository;
            _mapper = mapper;
        }

        public async Task<MejoraViewModel> Handle(GetMejoraByIdQuery query, CancellationToken cancellationToken)
        {
            var mejoras = await _mejoraRepository.GetAllAsync();
            var mejora = mejoras.FirstOrDefault(w => w.Id == query.Id);
            if (mejora == null) throw new Exception($"Mejora no encontrada");
            var mejoraVm = _mapper.Map<MejoraViewModel>(mejora);
            return mejoraVm;
        }
    }
}