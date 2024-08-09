using AutoMapper;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedades;
using RealEstateApp.Core.Application.ViewModels.TipoVentas;
using RealEstateApp.Core.Application.ViewModels.Mejoras;
using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.ViewModels.Users;
using RealEstateApp.Core.Application.Features.Propiedad.Queries.GetAllPropiedades;
using RealEstateApp.Core.Application.Features.Mejora.Commands.CreateMejora;
using RealEstateApp.Core.Application.Features.Mejora.Commands.UpdateMejora;
using RealEstateApp.Core.Application.Features.Mejora.Commands.DeleteMejoraById;
using RealEstateApp.Core.Application.Features.TipoPropiedad.Commands.UpdateTipoPropiedad;
using RealEstateApp.Core.Application.Features.TipoPropiedad.Commands.DeleteTipoPropiedadById;
using RealEstateApp.Core.Application.Features.TipoPropiedad.Commands.CreateTipoPropiedad;
using RealEstateApp.Core.Application.Features.TipoVenta.Commands.DeleteTipoVentaById;
using RealEstateApp.Core.Application.Features.TipoVenta.Commands.UpdateTipoVenta;
using RealEstateApp.Core.Application.Features.TipoVenta.Commands.CreateTipoVenta;
using RealEstateApp.Core.Application.ViewModels.Agentes;

namespace RealEstateApp.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {

            CreateMap<Propiedad, PropiedadViewModel>()
            .ForMember(x => x.Correo, opt => opt.Ignore())
            .ForMember(x => x.Telefono, opt => opt.Ignore())
            .ForMember(x => x.Foto, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<Propiedad, SavePropiedadViewModel>()
            .ReverseMap();

            CreateMap<TipoPropiedad, TipoPropiedadViewModel>();

            CreateMap<TipoPropiedad, SaveTipoPropiedadViewModel>()
            .ReverseMap();

            CreateMap<TipoVenta, TipoVentaViewModel>();

            CreateMap<TipoVenta, SaveTipoVentaViewModel>()
                .ReverseMap();

            CreateMap<Mejora, MejoraViewModel>();

            CreateMap<Mejora, SaveMejoraViewModel>()
                .ReverseMap();

            #region Mapping Api

            CreateMap<GetAllPropiedadesQuery, GetAllPropiedadesParameter>()
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Codigo));

            CreateMap<CreateMejoraCommand, Mejora>();
            CreateMap<DeleteMejoraByIdCommand, Mejora>();
            CreateMap<UpdateMejoraCommand, Mejora>();
            CreateMap<Mejora, MejoraUpdateResponse>();

            CreateMap<CreateTipoPropiedadCommand, TipoPropiedad>();
            CreateMap<DeleteTipoPropiedadByIdCommand, TipoPropiedad>();
            CreateMap<UpdateTipoPropiedadCommand, TipoPropiedad>();
            CreateMap<TipoPropiedad, TipoPropiedadUpdateResponse>();

            CreateMap<CreateTipoVentaCommand, TipoVenta>();
            CreateMap<DeleteTipoVentaByIdCommand, TipoVenta>();
            CreateMap<UpdateTipoVentaCommand, TipoVenta>();
            CreateMap<TipoVenta, TipoVentaUpdateResponse>();

            CreateMap<AgenteApi, AgenteViewModel>()
           .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
           .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
           .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<Propiedad, PropiedadDto>();

            #endregion

            #region Mapping Identity

            #region Athentication
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<AuthenticationResponse, AgenteViewModel>()
                .ForMember(dest => dest.Foto, opt => opt.MapFrom(src => src.ProfilePictureUrl))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.CantidadPropiedades, opt => opt.Ignore());

            CreateMap<AuthenticationResponse, PropiedadViewModel>()
                .ForMember(dest => dest.AgenteNombreCompleto, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.Telefono, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Foto, opt => opt.MapFrom(src => src.ProfilePictureUrl))
                .ForMember(dest => dest.AgenteId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignora la propiedad Id
                .ForMember(dest => dest.Tipo, opt => opt.Ignore())
                .ForMember(dest => dest.Codigo, opt => opt.Ignore())
                .ForMember(dest => dest.TipoVenta, opt => opt.Ignore())
                .ForMember(dest => dest.Valor, opt => opt.Ignore())
                .ForMember(dest => dest.CantidadHabitaciones, opt => opt.Ignore())
                .ForMember(dest => dest.CantidadBaños, opt => opt.Ignore())
                .ForMember(dest => dest.Tamaño, opt => opt.Ignore())
                .ForMember(dest => dest.Descripcion, opt => opt.Ignore())
                .ForMember(dest => dest.Mejoras, opt => opt.Ignore())
                .ForMember(dest => dest.Imagenes, opt => opt.Ignore())
                .ForMember(dest => dest.EsFavorita, opt => opt.Ignore());
            #endregion

            #region Register Request
            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(x => x.HasError, opt => opt.Ignore())
                .ForMember(x => x.Error, opt => opt.Ignore())
                .ForMember(x => x.File, opt => opt.Ignore())
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Rol))
                .ReverseMap();
            #endregion

            CreateMap<UserDto, UsuarioViewModel>()
            .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.EmailConfirmed))
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo))
            .ForMember(dest => dest.Contraseña, opt => opt.Ignore())
            .ReverseMap();

            CreateMap<UserDto, EditUsuarioViewModel>()
            .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.EmailConfirmed))
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo))
            .ForMember(dest => dest.Contraseña, opt => opt.Ignore())
            .ReverseMap();

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
