using Microsoft.Extensions.Configuration;
using Producto.Abstracciones.Interfaces.Reglas;

namespace Reglas
{
    public class Configuracion : IConfiguracion
    {

        private IConfiguration _configuration;

        public Configuracion(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string ObtenerMetodo(string seccion, string nombre)
        {
            var valor = _configuration[$"{seccion}:Metodos:{nombre}"];

            if (string.IsNullOrWhiteSpace(valor))
            {
                throw new Exception($"No se encontró el método '{nombre}' en la sección '{seccion}'.");
            }

            return valor;
        }

        public string ObtenerValor(string llave)
        {
            var valor = _configuration[llave];

            if (string.IsNullOrWhiteSpace(valor))
            {
                throw new Exception($"No se encontró la llave '{llave}' en la configuración.");
            }

            return valor;
        }
    }
}
