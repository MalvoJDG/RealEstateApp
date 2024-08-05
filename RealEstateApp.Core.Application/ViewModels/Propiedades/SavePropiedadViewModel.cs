using Microsoft.AspNetCore.Http;


namespace RealEstateApp.Core.Application.ViewModels.Propiedades
{
    public class SavePropiedadViewModel
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string? Codigo { get; set; }
        public string TipoVenta { get; set; }
        public decimal Valor { get; set; }
        public int CantidadHabitaciones { get; set; }
        public int CantidadBaños { get; set; }
        public float Tamaño { get; set; }
        public string Descripcion { get; set; }
        public string Mejoras { get; set; }
        public string? Imagenes { get; set; }
        public string? AgenteId { get; set; }
        public string? AgenteNombreCompleto { get; set; }

        public List<IFormFile> Files { get; set; }



    }
}
