using GraphQLAgentApp.Api.GraphQL;
using GraphQLAgentApp.Repository;
using GraphQLAgentApp.Service;
using GraphQLAgentApp.Mapper;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Use ContentRootPath to get the project directory (better approach)
var appDataPath = System.IO.Path.Combine(builder.Environment.ContentRootPath, "App_Data");
if (!Directory.Exists(appDataPath))
{
    Directory.CreateDirectory(appDataPath);
}
AppDomain.CurrentDomain.SetData("DataDirectory", appDataPath);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register AppDbContext with LocalDB, use logging from configuration
builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
    options.EnableSensitiveDataLogging();
});

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(BookMappingProfile).Assembly);
builder.Services.AddScoped<IMappingService, MappingService>();

// Register repository and service interfaces
builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();

// Add GraphQL services
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Map GraphQL endpoint
app.MapGraphQL("/graphql");

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");        

// Ensure database is created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
