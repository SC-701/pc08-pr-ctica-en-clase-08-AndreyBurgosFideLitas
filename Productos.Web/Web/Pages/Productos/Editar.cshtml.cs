using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Producto.Abstracciones.Interfaces.Reglas;
using Producto.Abstracciones.Modelos;
using System.Text;
using System.Text.Json;

namespace Web.Pages.Productos
{
    public class EditarModel : PageModel
    {
        private readonly IConfiguracion _configuracion;

        public EditarModel(IConfiguracion configuracion)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            string endpoint = _configuracion.ObtenerMetodo("ApiEndPoints", "EditarProductos");
            endpoint = string.Format(endpoint, Producto.Id);

            var productoRequest = new ProductoRequest
            {
                IdSubCategoria = Producto.IdSubCategoria,
                Nombre = Producto.Nombre,
                Descripcion = Producto.Descripcion,
                Precio = Producto.Precio,
                Stock = Producto.Stock,
                CodigoBarras = Producto.CodigoBarras
            };

            var json = JsonSerializer.Serialize(productoRequest);
            var contenido = new StringContent(json, Encoding.UTF8, "application/json");

            var cliente = new HttpClient();
            var respuesta = await cliente.PutAsync(endpoint, contenido);

            if (!respuesta.IsSuccessStatusCode)
            {
                var error = await respuesta.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, $"No se pudo editar el producto. Error: {error}");
                return Page();
            }

            TempData["Mensaje"] = "Producto editado correctamente.";
            return RedirectToPage("./Index");
        }
    }
}

