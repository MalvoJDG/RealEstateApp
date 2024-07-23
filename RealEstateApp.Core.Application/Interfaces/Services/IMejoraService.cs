using RealEstateApp.Core.Application.ViewModels.Mejoras;
using RealEstateApp.Core.Application.ViewModels.Propiedades;
using RealEstateApp.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IMejoraService : IGenericService<SaveMejoraViewModel, MejoraViewModel, Mejora>
    {

    }
}
