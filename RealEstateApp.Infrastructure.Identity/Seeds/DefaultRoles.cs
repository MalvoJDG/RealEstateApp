using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;

namespace RealEstateApp.Infraestructure.Identity.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {

            await roleManager.CreateAsync(new IdentityRole(Roles.AGENTE.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.ADMIN.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.CLIENTE.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.DESARROLADOR.ToString()));
        }
    }
}
