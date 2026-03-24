using Producto.Abstracciones.Interfaces.Reglas;
using Producto.Abstracciones.Interfaces.Servicios;
using Producto.Abstracciones.Modelos.Servicios.TipoDeCambio;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Servicios
{
    public class TipoDeCambioServicio : ITipoDeCambioServicio
    {   
        private readonly HttpClient _httpClient;
        private readonly IConfiguracion _configuracion;
        public TipoDeCambioServicio(HttpClient httpClient, IConfiguracion configuracion)
        {
            _httpClient = httpClient;
            _configuracion = configuracion;
        }

        public async Task<decimal> Obtener()
        {
            var fechaHoy = DateTime.Now.ToString("yyyy/MM/dd");

            var urlBase = _configuracion.ObtenerValor("ApiEndPointsTipoDeCambio:URLBase");
            var token = _configuracion.ObtenerValor("ApiEndPointsTipoDeCambio:BearerToken");

            var metodo = _configuracion.ObtenerMetodo(
                "ApiEndPointsTipoDeCambio",
                "ObtenerTipoDeCambio"
            );

            var urlFinal = urlBase + "/" + string.Format(metodo, fechaHoy, fechaHoy);

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(urlFinal);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error consultando el API del BCCR");
            }

            var json = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var resultado = JsonSerializer.Deserialize<TipoCambioResponse>(json, options);

            var tipoCambio = resultado?
                .Datos?
                .FirstOrDefault()?
                .Indicadores?
                .FirstOrDefault()?
                .Series?
                .FirstOrDefault()?
                .ValorDatoPorPeriodo ?? 0;

            return tipoCambio;
        }
    }
}

