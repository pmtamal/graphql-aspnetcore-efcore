using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQLAgentApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GraphQLAgentApp.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync() => await _context.Books.ToListAsync();
        public async Task<Book?> GetByIdAsync(int id) => await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
        public async Task<Book> AddAsync(string title, string author)
        {
            var book = new Book { Title = title, Author = author, CreatedAt = DateTime.UtcNow };
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return book;
        }
    }
}
