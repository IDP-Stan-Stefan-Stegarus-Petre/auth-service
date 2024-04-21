using MobyLabWebProgramming.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddCorsConfiguration()
    .AddAuthorizationWithSwagger("MobyLab Web App")
    .AddServices()
    .UseLogger()
    .AddWorkers()
    .AddApi();

var app = builder.Build();

app.ConfigureApplication();
app.Run();
