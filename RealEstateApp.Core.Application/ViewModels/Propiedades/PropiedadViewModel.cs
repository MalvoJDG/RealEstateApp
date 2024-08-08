namespace RealEstateApp.Core.Application.ViewModels.Propiedades
{
    public class PropiedadViewModel
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string Codigo { get; set; }
        public string TipoVenta { get; set; }
        public decimal Valor { get; set; }
        public int CantidadHabitaciones { get; set; }
        public int CantidadBaños { get; set; }
        public float Tamaño { get; set; }
        public string Descripcion { get; set; }
        public string Mejoras { get; set; }
        public string Imagenes { get; set; }
        public string AgenteId { get; set; }
        public string AgenteNombreCompleto { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Foto { get; set; }
        public bool EsFavorita { get; set; }

        public string ObtenerPrimeraImagen()
        {
                if (string.IsNullOrWhiteSpace(Imagenes))
                {
                    return null;
                }

                var imagenes = Imagenes.Split(';', StringSplitOptions.RemoveEmptyEntries);
                return imagenes.Length > 0 ? imagenes.Last().Trim() : null;
            
        }

        public List<string> ObtenerListaImagenes()
        {
            return Imagenes?.Split(';').ToList() ?? new List<string>();
        }
    }
}
