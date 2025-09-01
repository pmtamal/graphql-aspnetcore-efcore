using GraphQLAgentApp.Api.GraphQL;
using GraphQLAgentApp.Api.GraphQL.Queries;
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

builder.Services.AddScoped<AppDbContext>();

// Register AutoMapper
builder.Services.AddAutoMapper(typeof(BookMappingProfile).Assembly);
builder.Services.AddScoped<IMappingService, MappingService>();

        // Register repositories
        builder.Services.AddScoped<IBookRepository, BookRepository>();
        builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
        builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IUserProfileRepository, UserProfileRepository>();
        
        // Register services
        builder.Services.AddScoped<IBookService, BookService>();
        builder.Services.AddScoped<IAuthorService, AuthorService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IUserService, UserService>();

// GraphQL query classes are automatically resolved by HotChocolate

// Add GraphQL services
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddTypeExtension<BookQuery>()
    .AddTypeExtension<AuthorQuery>()
    .AddTypeExtension<CategoryQuery>()
    .AddTypeExtension<UserQuery>()
    .AddTypeExtension<AccountQuery>()
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .AddInMemorySubscriptions();

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

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
    
    // Seed data if database is empty
    if (!db.Authors.Any())
    {
        try
        {
            var author1 = new GraphQLAgentApp.Models.Entities.Author
            {
                FirstName = "John",
                LastName = "Doe",
                Nationality = "American",
                DateOfBirth = new DateTime(1980, 1, 1),
                Biography = "A prolific author"
            };
            var author2 = new GraphQLAgentApp.Models.Entities.Author
            {
                FirstName = "Jane",
                LastName = "Smith",
                Nationality = "British",
                DateOfBirth = new DateTime(1985, 5, 15),
                Biography = "Award-winning novelist"
            };
            
            db.Authors.AddRange(author1, author2);
            await db.SaveChangesAsync();
            
            var category1 = new GraphQLAgentApp.Models.Entities.Category
            {
                Name = "Fiction",
                Description = "Fictional literature"
            };
            var category2 = new GraphQLAgentApp.Models.Entities.Category
            {
                Name = "Non-Fiction",
                Description = "Non-fictional literature"
            };
            
            db.Categories.AddRange(category1, category2);
            await db.SaveChangesAsync();
            
            var book1 = new GraphQLAgentApp.Models.Entities.Book
            {
                Title = "The Great Adventure",
                AuthorId = author1.Id,
                CategoryId = category1.Id,
                ISBN = "978-1234567890",
                Description = "An exciting adventure story",
                PublicationYear = 2020,
                Publisher = "Adventure Press",
                Pages = 300,
                Language = "English",
                Price = 19.99m,
                StockQuantity = 10,
                IsAvailable = true
            };
            var book2 = new GraphQLAgentApp.Models.Entities.Book
            {
                Title = "Science Today",
                AuthorId = author2.Id,
                CategoryId = category2.Id,
                ISBN = "978-0987654321",
                Description = "A comprehensive guide to modern science",
                PublicationYear = 2021,
                Publisher = "Science Books",
                Pages = 450,
                Language = "English",
                Price = 29.99m,
                StockQuantity = 5,
                IsAvailable = true
            };
            
            db.Books.AddRange(book1, book2);
            await db.SaveChangesAsync();
            
            Console.WriteLine("Database seeded successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error seeding database: {ex.Message}");
        }
    }
    
    // Ensure admin user exists
    if (!db.Users.Any(u => u.Username == "admin"))
    {
        try
        {
            var adminUser = new GraphQLAgentApp.Models.Entities.User
            {
                Username = "admin",
                Email = "admin@bookstore.com",
                PasswordHash = "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", // "admin123" hashed with SHA256
                IsAdmin = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
            
            db.Users.Add(adminUser);
            await db.SaveChangesAsync();
            
            // Create admin profile
            var adminProfile = new GraphQLAgentApp.Models.Entities.UserProfile
            {
                UserId = adminUser.Id,
                FirstName = "Admin",
                LastName = "User",
                Phone = "+1-555-0123",
                Address = "123 Admin Street",
                City = "Admin City",
                State = "AS",
                PostalCode = "12345",
                Country = "United States",
                CreatedAt = DateTime.UtcNow
            };
            
            db.UserProfiles.Add(adminProfile);
            await db.SaveChangesAsync();
            
            Console.WriteLine("Admin user created successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating admin user: {ex.Message}");
        }
    }
}

// Ensure database is created and seeded



app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
