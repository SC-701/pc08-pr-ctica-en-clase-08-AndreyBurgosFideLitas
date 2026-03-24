using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Producto.Abstracciones.Interfaces.Reglas;
using Producto.Abstracciones.Modelos;
using System.Text.Json;
using System.Threading.Tasks;

namespace Web.Pages.Productos
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguracion _configuracion;
        public IList<ProductoResponse> Productos { get; set; } = default!;
        public IndexModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        public async Task OnGet()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerProductos");
            var cliente = new HttpClient();
            var solicitud = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var respuesta=await cliente.SendAsync(solicitud);
            respuesta.EnsureSuccessStatusCode();
            var resultado=await respuesta.Content.ReadAsStringAsync();
            var opciones = new JsonSerializerOptions 
                {PropertyNameCaseInsensitive = true};
            Productos = JsonSerializer.Deserialize<List<ProductoResponse>>(resultado, opciones);
        }
    }
}
