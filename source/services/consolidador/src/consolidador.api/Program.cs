using Application;
using Infrastructure;
using Persistence;
using Api.Middlewares;
using Api.Extensions;
using Api.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.ApplyMigrations();
app.ConfigureIntegrationEventHandlers();

app.MapControllers();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapGroup("api/saldoDiario").MapSaldoDiarioEndpoints();

app.Run();

public partial class Program { }
