using Producto.Abstracciones.Interfaces.DA;
using Producto.Abstracciones.Interfaces.Flujo;
using Producto.Abstracciones.Interfaces.Reglas;
using Producto.Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producto.Flujo
{
    public class ProductoFlujo : IProductoFlujo
    {
        private IProductoDA _productoDA;
        private IProductoReglas _productoReglas;

        public ProductoFlujo(IProductoDA productoDA, IProductoReglas productoReglas)
        {
            _productoDA = productoDA;
            _productoReglas = productoReglas;
        }

        public Task<Guid> Agregar(ProductoRequest producto)
        {
            return _productoDA.Agregar(producto);
        }

        public Task<Guid> Editar(Guid Id, ProductoRequest producto)
        {
            return _productoDA.Editar(Id, producto);
        }

        public Task<Guid> Eliminar(Guid Id)
        {
            return _productoDA.Eliminar(Id);
        }

        public Task<IEnumerable<ProductoResponse>> ObtenerProductos()
        {
            return _productoDA.ObtenerProductos();
        }

        public async Task<ProductoDetalle> ObtenerProducto(Guid Id)
        {
            var producto = await _productoDA.ObtenerProducto(Id);

            if (producto == null)
                return null;

            producto.PrecioUSD = await _productoReglas.CalcularPrecioUSD(producto.Precio);

            return producto;
        }
    }
}
