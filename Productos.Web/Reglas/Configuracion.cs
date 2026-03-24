using Microsoft.Extensions.Configuration;
using Producto.Abstracciones.Interfaces.Reglas;

namespace Reglas
{
    public class Configuracion : IConfiguracion
    {
        private readonly IConfiguration _configuration;

        public Configuracion(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ObtenerMetodo(string seccion, string nombre)
        {
            string? valor = _configuration[$"{seccion}:{nombre}"];

            if (string.IsNullOrEmpty(valor))
            {
                throw new Exception($"No se encontró el método '{nombre}' en la sección '{seccion}'.");
            }

            return valor;
        }

        public string ObtenerValor(string llave)
        {
            string? valor = _configuration[llave];

            if (string.IsNullOrEmpty(valor))
            {
                throw new Exception($"No se encontró la llave '{llave}'.");
            }

            return valor;
        }
    }
}