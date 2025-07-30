using Microsoft.OpenApi.Models;
using Order.Delivery.Email.Service.Services;
using Order.Delivery.Email.Service.Services.Interfaces;
using System.Reflection;

namespace Order.Delivery.Email.Service.Extensions;

public static class EmailServiceBuilderExtensions
{
    public static void AddSwaggerDoc(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Serviço de envio de emails",
                Description = "API feita com .NET Core 8 e SMTP",
                Contact = new OpenApiContact
                {
                    Name = "Página de contato",
                    Url = new Uri("https://www.google.com")
                },
                License = new OpenApiLicense
                {
                    Name = "Licenciamento",
                    Url = new Uri("https://www.google.com")
                }
            });
            var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
        });
    }

    public static void AddServicesConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IEmailService, EmailService>();
    }
}
