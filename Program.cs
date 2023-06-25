global using ServoEscolarWebApi.Clases;
global using ServoEscolarWebApi.Datos.Consultas;
global using ServoEscolarWebApi.Datos.Entidades;
global using ServoEscolarWebApi.Datos.Repositorios;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;

namespace ServoEscolarWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //builder.Services.AddDbContext<ServoEscolarWebApiContext>(options =>
            //    options.UseSqlServer(builder.Configuration.GetConnectionString("ServoEscolarWebApiContext") ?? throw new InvalidOperationException("Connection string 'ServoEscolarWebApiContext' not found.")));

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddScoped(typeof(IRepositorio<Alumno, AlumnoConsulta, string>), typeof(AlumnosRepositorio));
            builder.Services.AddScoped(typeof(IRepositorio<CicloEscolar, CicloEscolarConsulta, int>), typeof(CiclosEscolaresRepositorio));
            builder.Services.AddScoped(typeof(IRepositorio<Familia, FamiliaConsulta, string>), typeof(FamiliasRepositorio));
            builder.Services.AddScoped(typeof(IRepositorio<Inscripcion, InscripcionConsulta, int>), typeof(InscripcionesRepositorio));

           

            builder.Services.AddAuthentication("ApiKey")
                .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKey", null);

            var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var apiKeyHeader = configuration.GetSection("AppSettings:ApiKeyHeader").Value;

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.ApiKey,
                    Name = apiKeyHeader,
                    In = ParameterLocation.Header,
                    Description = "API Key authorization"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "ApiKey"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                options.RoutePrefix = "swagger";
                options.DisplayRequestDuration();
            });

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}