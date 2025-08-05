using GraphQLAgentApp.Mapper;
using GraphQLAgentApp.Models.Dtos;
using GraphQLAgentApp.Models.Entities;
using GraphQLAgentApp.Repository;

namespace GraphQLAgentApp.Service
{
    public class BookService(IBookRepository repository, IMappingService mappingService) : IBookService
    {
        public async Task<List<BookDto>> GetAllAsync()
        {
            var books = await repository.GetAllAsync();
            return mappingService.MapList<Book, BookDto>(books);
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await repository.GetByIdAsync(id);
            if (book == null) return null;
            return mappingService.Map<Book, BookDto>(book);
        }

        public async Task<BookDto> AddAsync(string title, int authorId, int categoryId, string isbn, string description, int publicationYear, string publisher, int pages, string language, decimal price, int stockQuantity)
        {
            var book = await repository.AddAsync(title, authorId, categoryId, isbn, description, publicationYear, publisher, pages, language, price, stockQuantity);
            return mappingService.Map<Book, BookDto>(book);
        }
    }
}
