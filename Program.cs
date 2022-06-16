using Serilog;

var appBuilder = WebApplication.CreateBuilder(args);

var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

IConfigurationRoot configuration = builder.Build();

// Add services to the container.

using var log = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

appBuilder.Services.AddControllers();

appBuilder.Services.AddRazorPages();
appBuilder.Services.AddLogging();
appBuilder.Services.AddHttpClient();
appBuilder.Services.AddConnections();

appBuilder.Services.AddEndpointsApiExplorer();
appBuilder.Services.AddSwaggerGen();

var app = appBuilder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.MapControllers();

app.Run();
