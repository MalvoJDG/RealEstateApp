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
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.ViewModels.Users;

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

            #region Mapping Identity

            #region Athentication Request
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            #endregion

            #region Register Request
            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ForMember(x => x.File, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region Forgotviewmodel
            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #region reset password
            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();
            #endregion

            #endregion
        }
    }
}
