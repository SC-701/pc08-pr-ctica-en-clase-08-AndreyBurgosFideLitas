using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Producto.Abstracciones.Interfaces.API;
using Producto.Abstracciones.Interfaces.Flujo;
using Producto.Abstracciones.Modelos;

namespace Producto.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase, IProductoController
    {
        private IProductoFlujo _productoFlujo;
        private ILogger<ProductoController> _logger;

        public ProductoController(IProductoFlujo productoFlujo, ILogger<ProductoController> logger)
        {
            _productoFlujo = productoFlujo;
            _logger = logger;
        }

        #region Operaciones

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] ProductoRequest producto)
        {
            Guid resultado = await _productoFlujo.Agregar(producto);

            return CreatedAtAction(
                nameof(ObtenerProducto),
                new { Id = resultado },
                producto
            );
        }
        [HttpPut("{Id}")]
        public async Task<IActionResult> Editar(Guid Id, ProductoRequest producto)
        {
            if (await VerificarProductoExiste(Id))
                return NotFound("El Producto no existe");
            var resultado = await _productoFlujo.Editar(Id, producto);
            return Ok(resultado);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar(Guid Id)
        {
            if (await VerificarProductoExiste(Id))
                return NotFound("El Producto no existe");
            var resultado = await _productoFlujo.Eliminar(Id);
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerProductos()
        {
            var resultado = await _productoFlujo.ObtenerProductos();
            if (!resultado.Any())
                return NoContent();
            return Ok(resultado);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> ObtenerProducto([FromRoute]Guid Id)
        {
            var resultado = await _productoFlujo.ObtenerProducto(Id);

            return Ok(resultado);
            //testmessage//
        }
        #endregion Operaciones

        #region Helpers
        private async Task<bool> VerificarProductoExiste(Guid Id)
        {
            var resultadoValidacion = false;
            var resultadoProductoExiste = await _productoFlujo.ObtenerProducto(Id);
            if (resultadoProductoExiste == null)
                resultadoValidacion = true;
            return resultadoValidacion;

        }
        #endregion Helpers

    }
}
