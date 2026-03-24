using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Producto.Abstracciones.Interfaces.Reglas;
using Producto.Abstracciones.Modelos;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Productos
{
    public class AgregarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public AgregarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ProductoRequest Producto { get; set; } = new ProductoRequest();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "AgregarProducto");

            var cliente = new HttpClient();

            var json = JsonSerializer.Serialize(Producto);
            Console.WriteLine("JSON enviado: " + json);

            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            var respuesta = await cliente.PostAsync(endpoint, contenido);

            var respuestaTexto = await respuesta.Content.ReadAsStringAsync();
            Console.WriteLine("Status Code: " + respuesta.StatusCode);
            Console.WriteLine("Respuesta API: " + respuestaTexto);

            if (!respuesta.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, $"No se pudo guardar el producto. Error: {respuestaTexto}");
                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
