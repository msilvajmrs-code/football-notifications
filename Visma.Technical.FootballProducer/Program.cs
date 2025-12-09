using Scalar.AspNetCore;
using Visma.Technical.Core.Contracts;
using Visma.Technical.Core.Features;
using Visma.Technical.Core.Features.ProcessFootballEvent;
using Visma.Technical.Core.Features.ProcessFootballEvent.InputHandlers;
using Visma.Technical.Core.Features.PublishFootballEvent.InputHandlers;
using Visma.Technical.Core.Infrastructure.Data;
using Visma.Technical.Core.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IGameRepository, FakeGameRepository>();
builder.Services.AddScoped<IMqPublisher, RabbitMqPublisher>();
builder.Services.AddScoped<IProcessFootballEvent, ProcessFootballEvent>();
builder.Services.RegisterInputHandlersAsKeyedServices();
builder.Services.AddSingleton("amqp://guest:guest@localhost:5672/");
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
