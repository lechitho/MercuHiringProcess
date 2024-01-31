using Application.Abstractions.Caching;
using Infastructure.Caching;
using Infastructure;
using Serilog;
using Application.Candidates.Queries;
using MediatR;
using Domain.Abstractions;
using Infastructure.Persistent;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(GetCandidatesQueryHandler).Assembly));

builder.Services.AddSingleton(builder.Configuration.GetRequiredSection(nameof(RepositoryOptions)).Get<RepositoryOptions>());
builder.Services.AddSingleton(builder.Configuration.GetRequiredSection(nameof(CacheOptions)).Get<CacheOptions>());

builder.Services.AddTransient<ICandidateRepository, JsonCandidateRepository>();
builder.Services.AddTransient<ICacheService, CacheService>();

builder.Services.AddMemoryCache();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj} {NewLine}{Exception}")
    .WriteTo.File(
                    path: "logs/.log",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj} {NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day,
                    rollOnFileSizeLimit: true,
                    fileSizeLimitBytes: 1024 * 1024 * 10)
    .CreateLogger();

builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.ClearProviders();
    loggingBuilder.AddSerilog();
});
builder.Services.AddCors(p => p.AddPolicy("hiringProcessing", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("hiringProcessing");

app.UseAuthorization();

app.MapControllers();

app.Run();
