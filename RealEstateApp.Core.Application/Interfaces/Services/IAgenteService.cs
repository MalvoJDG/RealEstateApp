using RealEstateApp.Core.Application.ViewModels.Agentes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAgenteService
    {
        Task<IEnumerable<AgenteViewModel>> GetAgentesAsync();
        Task<AgenteViewModel> GetAgenteByIdAsync(string id);
        Task<bool> ChangeEmailConfirmedStatusAsync(string id, bool isConfirmed);
    }
}
