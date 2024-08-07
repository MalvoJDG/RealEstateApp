using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Enums;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModels.Users
{
    public class SaveUserViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Debe colocar el nombre del usuario")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe colocar el apellido del usuario")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Debe colocar un nombre de usuario")]
        [DataType(DataType.Text)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coiciden")]
        [Required(ErrorMessage = "Debe colocar una contraseña")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Debe colocar un correo")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe colocar un telefono")]
        
        [DataType(DataType.Text)]
        public string Phone { get; set; }

        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Debe colocar una foto de perfil")]
        public IFormFile File { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public Roles Tipo { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
