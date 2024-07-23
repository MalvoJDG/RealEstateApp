﻿using Microsoft.AspNetCore.Identity;

namespace RealEstateApp.Infraestructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}
