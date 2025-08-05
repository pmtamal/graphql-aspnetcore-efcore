using GraphQLAgentApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLAgentApp.Repository
{
    public class CategoryRepository(AppDbContext context) : ICategoryRepository
    {
        public async Task<List<Category>> GetAllAsync() => await context.Categories
            .Include(c => c.Books)
            .ToListAsync();
            
        public async Task<Category?> GetByIdAsync(int id) => await context.Categories
            .Include(c => c.Books)
            .FirstOrDefaultAsync(c => c.Id == id);
            
        public async Task<Category> AddAsync(string name, string? description)
        {
            var category = new Category 
            { 
                Name = name,
                Description = description,
                CreatedAt = DateTime.UtcNow 
            };
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
            return category;
        }
    }
} 