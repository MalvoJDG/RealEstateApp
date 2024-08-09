using System.ComponentModel.DataAnnotations;


namespace RealEstateApp.Core.Application.ViewModels.Users
{
    public class EditUsuarioViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Debe colocar su nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Debe colocar el apellido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Debe colocar el correo")]
        [EmailAddress]
        public string Correo { get; set; }

        [Required(ErrorMessage = "Debe colocar el nombre de usuario")]
        public string NombreUsuario { get; set; }

        public string? Tipo { get; set; }

        [DataType(DataType.Password)]
        public string? Contraseña { get; set; }

        [DataType(DataType.Password)]
        [Compare("Contraseña", ErrorMessage = "Las contraseñas no coinciden")]
        public string? ConfirmarContraseña { get; set; }
        public bool Estado { get; set; }
        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
