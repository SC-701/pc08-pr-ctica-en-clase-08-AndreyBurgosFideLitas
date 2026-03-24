using Dapper;
using Microsoft.Data.SqlClient;
using Producto.Abstracciones.Interfaces.DA;
using Producto.Abstracciones.Modelos;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Producto.DA
{
    public class ProductoDA : IProductoDA
    {
        private IRepositorioDapper _repositorioDapper;
        private SqlConnection _sqlConnection;



        public ProductoDA(IRepositorioDapper repositorioDapper)
        {
            _repositorioDapper = repositorioDapper;
            _sqlConnection = _repositorioDapper.ObtenerRepositorio();
        }
        public async Task<Guid> Agregar(ProductoRequest producto)
        {
            string query = @"AgregarProducto";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Guid.NewGuid(),
                IdSubCategoria = producto.IdSubCategoria,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                Precio = producto.Precio,
                Stock = producto.Stock,
                CodigoBarras = producto.CodigoBarras
            },
            commandType: CommandType.StoredProcedure

            );

            return resultadoConsulta;
        }

        public async Task<Guid> Editar(Guid id, ProductoRequest producto)
        {
            await VerificarProductoExiste(id);

            string query = "EditarProducto";

            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(
                query,
                new
                {
                    Id = id,
                    IdSubCategoria = producto.IdSubCategoria,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    Precio = producto.Precio,
                    Stock = producto.Stock,
                    CodigoBarras = producto.CodigoBarras
                },
                commandType: CommandType.StoredProcedure
            );

            return resultadoConsulta;
        }
        public async Task<Guid> Eliminar(Guid Id)
        {
            await VerificarProductoExiste(Id);
            string query = @"EliminarProducto";
            var resultadoConsulta = await _sqlConnection.ExecuteScalarAsync<Guid>(query, new
            {
                Id = Id
            },
            commandType: CommandType.StoredProcedure
            );

            return resultadoConsulta;

        }

        public async Task<IEnumerable<ProductoResponse>> ObtenerProductos()
        {
            string query = @"ObtenerProductos";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoResponse>(query);
            return resultadoConsulta;
        }

        public async Task<ProductoDetalle> ObtenerProducto(Guid Id)
        {
            string query = @"ObtenerProducto";
            var resultadoConsulta = await _sqlConnection.QueryAsync<ProductoDetalle>(query, new { id = Id });
            return resultadoConsulta.FirstOrDefault();
        }
        private async Task VerificarProductoExiste(Guid Id)
        {
            ProductoResponse? resultadoConsultaProducto = await ObtenerProducto(Id);
            if (resultadoConsultaProducto == null)
                throw new Exception("El producto no existe");
        }
    }

}
