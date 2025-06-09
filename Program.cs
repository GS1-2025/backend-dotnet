using System.Reflection;
using FluentValidation;
using gs_sensolux.Application.DTOs.Request;
using gs_sensolux.Application.UseCases;
using gs_sensolux.Application.Validators;
using gs_sensolux.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace gs_sensolux
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseOracle(builder.Configuration.GetConnectionString("OracleSensoLux")));

            builder.Services.AddControllers();


            // REGISTRO DO USE CASE
            builder.Services.AddScoped<EnderecoUseCase>();
            builder.Services.AddScoped<PedidoUseCase>();
            builder.Services.AddScoped<ProdutoUseCase>();
            builder.Services.AddScoped<SensorUseCase>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API gs_sensolux",
                    Version = "v1",
                    Description = "Documentação da API com comentários XML"
                });
            });

            builder.Services.AddScoped<IValidator<CreateEnderecoRequest>, CreateEnderecoRequestValidator>();
            builder.Services.AddScoped<IValidator<CreatePedidoRequest>, CreatePedidoRequestValidator>();
            builder.Services.AddScoped<IValidator<CreateProdutoRequest>, CreateProdutoRequestValidator>();
            builder.Services.AddScoped<IValidator<CreateSensorRequest>, CreateSensorRequestValidator>();

            var app = builder.Build();

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
