namespace RealEstateApp.Core.Application.Dtos.Account
{
    public class UserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Tipo { get; set; }
        public bool? Estado { get; set; }
        public bool EmailConfirmed { get; set; }

    }
}
