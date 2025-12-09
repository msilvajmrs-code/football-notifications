using Microsoft.AspNetCore.Diagnostics;
using Scalar.AspNetCore;
using Visma.Technical.Core.Contracts;
using Visma.Technical.Core.Features;
using Visma.Technical.Core.Features.ProcessFootballEvent;
using Visma.Technical.Core.Features.ProcessFootballEvent.InputHandlers;
using Visma.Technical.Core.Infrastructure.Data;
using Visma.Technical.Core.Infrastructure.Messaging;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration configuration = builder.Configuration;
var rabbitConfig = configuration.GetSection("RabbitMq").Get<RabbitMqConfiguration>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IGameRepository, FakeGameRepository>();
builder.Services.AddScoped<IMqPublisher, RabbitMqPublisher>();
builder.Services.AddScoped<IProcessFootballEvent, ProcessFootballEvent>();
builder.Services.RegisterInputHandlersAsKeyedServices();
builder.Services.AddSingleton(rabbitConfig ?? new RabbitMqConfiguration());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
if (!app.Environment.IsDevelopment())
{
    // exterme over simplification - just to hide exception details from clients
    app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = Text.Plain;
            await context.Response.WriteAsync("Error");
        });
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
