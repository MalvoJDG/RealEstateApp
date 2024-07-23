using Microsoft.AspNetCore.Identity;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Infraestructure.Identity.Entities;

namespace RealEstateApp.Infraestructure.Identity.Seeds
{
    public static class DefaultDesarrolladorUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {

            ApplicationUser defaultuser = new()
            {
                UserName = "Desarrolador",
                Email = "Desa@gmail.com",
                FirstName = "Jhon",
                LastName = "Doe",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultuser.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultuser.Email);

                if(user == null)
                {
                   await userManager.CreateAsync(defaultuser, "123Pa$$word!");
                   await userManager.AddToRoleAsync(defaultuser, Roles.DESARROLADOR.ToString());
                }
            }
        }
    }
}
