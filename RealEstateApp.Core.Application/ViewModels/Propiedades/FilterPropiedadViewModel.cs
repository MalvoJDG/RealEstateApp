namespace RealEstateApp.Core.Application.ViewModels.Propiedades
{
    public class FilterPropiedadViewModel
    {
        public string? Tipo { get; set; }
        public decimal? PrecioMinimo { get; set; }
        public decimal? PrecioMaximo { get; set; }
        public int? CantidadHabitaciones { get; set; }
        public int? CantidadBaños { get; set; }

        public string? Searchtearm { get; set; }
    }
}
