using AutoMapper;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedades;
using RealEstateApp.Core.Application.ViewModels.TipoVentas;
using RealEstateApp.Core.Application.ViewModels.Mejoras;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile() 
        {

            CreateMap<Propiedad, PropiedadViewModel>();

            CreateMap<Propiedad, SavePropiedadViewModel>()
            .ReverseMap();

            CreateMap<TipoPropiedad, TipoPropiedadViewModel>();

            CreateMap<TipoPropiedad, SaveTipoPropiedadViewModel>()
            .ReverseMap();

            CreateMap<TipoVenta, TipoVenta>();

            CreateMap<TipoVenta, SaveTipoVentaViewModel>()
                .ReverseMap();

            CreateMap<Mejora, MejoraViewModel>();

            CreateMap<Mejora, SaveMejoraViewModel>()
                .ReverseMap();
        }
    }
}
