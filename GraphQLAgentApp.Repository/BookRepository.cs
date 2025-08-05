using System;
using GraphQLAgentApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLAgentApp.Repository
{
    public class BookRepository(AppDbContext context) : IBookRepository
    {
        public async Task<List<Book>> GetAllAsync() => await context.Books
            .Include(b => b.Author)
            .Include(b => b.Category)
            .ToListAsync();
            
        public async Task<Book?> GetByIdAsync(int id) => await context.Books
            .Include(b => b.Author)
            .Include(b => b.Category)
            .FirstOrDefaultAsync(b => b.Id == id);
            
        public async Task<Book> AddAsync(string title, int authorId, int categoryId, string isbn, string description, int publicationYear, string publisher, int pages, string language, decimal price, int stockQuantity)
        {
            var book = new Book 
            { 
                Title = title, 
                AuthorId = authorId,
                CategoryId = categoryId,
                ISBN = isbn,
                Description = description,
                PublicationYear = publicationYear,
                Publisher = publisher,
                Pages = pages,
                Language = language,
                Price = price,
                StockQuantity = stockQuantity,
                IsAvailable = true,
                CreatedAt = DateTime.UtcNow 
            };
            await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
            return book;
        }
    }
}
