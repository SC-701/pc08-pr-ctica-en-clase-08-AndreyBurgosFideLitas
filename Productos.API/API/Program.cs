using Producto.Abstracciones.Interfaces.DA;
using Producto.Abstracciones.Interfaces.Flujo;
using Producto.Abstracciones.Interfaces.Reglas;
using Producto.Abstracciones.Interfaces.Servicios;
using Producto.DA;
using Producto.DA.Repositorios;
using Producto.Flujo;
using Reglas;
using Servicios;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IProductoFlujo, ProductoFlujo>();
            builder.Services.AddScoped<IProductoDA, ProductoDA>();
            builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();

            builder.Services.AddScoped<IProductoReglas, ProductoReglas>();
            builder.Services.AddHttpClient<ITipoDeCambioServicio, TipoDeCambioServicio>();
            builder.Services.AddScoped<IConfiguracion, Configuracion>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
