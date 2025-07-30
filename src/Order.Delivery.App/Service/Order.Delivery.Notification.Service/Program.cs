using Order.Delivery.Notification.Service;
using Order.Delivery.Notification.Service.Services.Interfaces;
using Refit;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddRefitClient<IEmailService>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(builder.Configuration["Services:EmailApiUrl"]!);
    });

var host = builder.Build();
host.Run();
