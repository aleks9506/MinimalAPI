using ToDo.Api.Endpoints;
using ToDo.Infrasctructure;
using ToDo.Application;
using System.Text.Json.Serialization;
using Microsoft.FeatureManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices();
builder.Services.AddDataRepositories();
builder.Services.AddExceptionHandler(builder.Environment);
builder.Services.AddFeatureManagement();

builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.NumberHandling = JsonNumberHandling.Strict;
});

builder.Services.AddToDbContext(
        builder.Configuration
    );

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddValidation();

builder.Host.AddSerilogLogging();

var app = builder.Build();

if(!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

app.MapToDoTaskEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.DocumentTitle = "My API Documentation";
        options.SwaggerEndpoint("/openapi/v1.json", "Api V1");
    });
}

app.UseHttpsRedirection();

app.Run();
