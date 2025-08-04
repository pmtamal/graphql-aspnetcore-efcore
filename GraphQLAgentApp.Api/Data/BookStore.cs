// In-memory data store for Book
using GraphQLAgentApp.Models.Entities;

namespace GraphQLAgentApp.Api.Data
{
    public static class BookStore
    {
        private static readonly List<Book> _books = new();
        private static int _nextId = 1;

        public static IEnumerable<Book> GetAll() => _books;
        public static Book? GetById(int id) => _books.Find(b => b.Id == id);
        public static Book Add(string title, string author)
        {
            var book = new Book { Id = _nextId++, Title = title, Author = author };
            _books.Add(book);
            return book;
        }
    }
}