using System.Linq;
using GraphQLAgentApp.Models.Entities;

namespace GraphQLAgentApp.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public IQueryable<Book> GetAll() => _context.Books;
        public Book? GetById(int id) => _context.Books.FirstOrDefault(b => b.Id == id);
        public Book Add(string title, string author)
        {
            var book = new Book { Title = title, Author = author, CreatedAt = DateTime.UtcNow };
            _context.Books.Add(book);
            _context.SaveChanges();
            return book;
        }
    }
}
