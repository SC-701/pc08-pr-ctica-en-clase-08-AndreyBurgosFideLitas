using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Producto.Abstracciones.Interfaces.Reglas;
using Producto.Abstracciones.Modelos;
using System.Text.Json;

namespace Web.Pages.Productos
{
    public class EliminarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EliminarModel(IConfiguracion configuracion)
        {
            _configuracion = configuracion;
        }

        [BindProperty]
        public ProductoDetalle Producto { get; set; } = new ProductoDetalle();

        public async Task<IActionResult> OnGet(Guid id)
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "ObtenerProducto");
            endpoint = string.Format(endpoint, id);

            var cliente = new HttpClient();
            var respuesta = await cliente.GetAsync(endpoint);

            if (!respuesta.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }

            var resultado = await respuesta.Content.ReadAsStringAsync();

            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            Producto = JsonSerializer.Deserialize<ProductoDetalle>(resultado, opciones);

            if (Producto == null)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EliminarProductos");
            endpoint = string.Format(endpoint, Producto.Id);

            var cliente = new HttpClient();
            var respuesta = await cliente.DeleteAsync(endpoint);

            if (!respuesta.IsSuccessStatusCode)
            {
                var error = await respuesta.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"No se pudo eliminar el producto. Error: {error}");
                return Page();
            }

            TempData["Mensaje"] = "Producto eliminado correctamente.";
            return RedirectToPage("./Index");
        }
    }
}

