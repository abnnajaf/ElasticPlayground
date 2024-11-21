using ElasticPlayground.Extensions;
using ElasticPlayground.Logging;
using ElasticPlayground.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ExampleService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Use Serilog as the logging provider
builder.Host.ConfigureSerilog(builder.Configuration);



var app = builder.Build();

app.UseTraceIdFilter();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapEndpoints();

app.Run();