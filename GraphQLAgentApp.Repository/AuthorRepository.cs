using GraphQLAgentApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLAgentApp.Repository
{
    public class AuthorRepository(AppDbContext context) : IAuthorRepository
    {
        public async Task<List<Author>> GetAllAsync() => await context.Authors
            .Include(a => a.Books)
            .ToListAsync();
            
        public async Task<Author?> GetByIdAsync(int id) => await context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.Id == id);
            
        public async Task<Author> AddAsync(string firstName, string lastName, string? biography, DateTime? dateOfBirth, string? nationality, string? website)
        {
            var author = new Author 
            { 
                FirstName = firstName,
                LastName = lastName,
                Biography = biography,
                DateOfBirth = dateOfBirth,
                Nationality = nationality,
                Website = website,
                CreatedAt = DateTime.UtcNow 
            };
            await context.Authors.AddAsync(author);
            await context.SaveChangesAsync();
            return author;
        }
    }
} 