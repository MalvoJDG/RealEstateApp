using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Features.Propiedad.Queries.GetAllPropiedades
{
    /// <summary>
    /// Parámetros para filtrar las propiedades por codigo
    /// </summary>  
    public class GetAllPropiedadesParameter
    {
        /// <example>123456</example>
        [SwaggerParameter(Description = "Colocar el codigo de la propiedad por la cual quiere filtrar")]
        public string Codigo { get; set; }
    }
}
