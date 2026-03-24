using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producto.Abstracciones.Modelos
{
    public class ProcuctoBase
    {
        [Required(ErrorMessage = "La propiedad nombre es requerida")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La propiedad Descripcion es requerida ")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La propiedad Precio es requerida")]
        public decimal Precio { get; set; }
        [Required(ErrorMessage = "La propiedad Stock es requerida")]
        public int Stock { get; set; }
        [Required(ErrorMessage = "La propiedad CodigoBarras es requerida")]
        [RegularExpression(@"[0-9]{13}", ErrorMessage = "El código de barras debe tener 13 números")]
        public string CodigoBarras { get; set; }
    }
    public class ProductoRequest : ProcuctoBase
    {
        public Guid IdSubCategoria { get; set; }
    }
    public class ProductoResponse : ProcuctoBase
    {
        public Guid Id { get; set; }
        public Guid IdSubCategoria { get; set; }
        public string? SubCategoria { get; set; }
        public string? Categoria { get; set; }
    }

    public class ProductoDetalle : ProductoResponse
    {
        public decimal PrecioUSD { get; set; }
    }
}
