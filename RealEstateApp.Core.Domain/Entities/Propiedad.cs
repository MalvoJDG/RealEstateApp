using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Domain.Entities
{
    public class Propiedad
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
        public string? Mejoras { get; set; } 
        public string Imagenes { get; set; }
        public string AgenteId { get; set; }
    }
}
