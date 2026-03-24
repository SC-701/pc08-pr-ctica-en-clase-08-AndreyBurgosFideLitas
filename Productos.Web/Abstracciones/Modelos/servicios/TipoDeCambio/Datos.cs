using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Producto.Abstracciones.Modelos.Servicios.TipoDeCambio
{
    public class TipoCambioResponse
    {   
        public bool Estado { get; set; }
        public string Mensaje { get; set; } = string.Empty;
        public List<TipoCambioDato> Datos { get; set; } = new List<TipoCambioDato>();
    }

    public class TipoCambioDato
    {
        public string Titulo { get; set; } = string.Empty;
        public string Periodicidad { get; set; } = string.Empty;
        public List<TipoCambioIndicador> Indicadores { get; set; } = new List<TipoCambioIndicador>();
    }

    public class TipoCambioIndicador
    {
        public string CodigoIndicador { get; set; } = string.Empty;
        public string NombreIndicador { get; set; } = string.Empty;
        public List<TipoCambioSerie> Series { get; set; } = new List<TipoCambioSerie>();
    }

    public class TipoCambioSerie
    {
        public DateTime Fecha { get; set; }
        public decimal ValorDatoPorPeriodo { get; set; }
    }
}


