using Microsoft.AspNetCore.Mvc;
using Producto.Abstracciones.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producto.Abstracciones.Interfaces.API
{
    public interface IProductoController
    {
        Task<IActionResult> ObtenerProductos();
        Task<IActionResult> ObtenerProducto(Guid Id);
        Task<IActionResult> Agregar(ProductoRequest producto);
        Task<IActionResult> Editar(Guid Id, ProductoRequest producto);
        Task<IActionResult> Eliminar(Guid Id);
    }
}
