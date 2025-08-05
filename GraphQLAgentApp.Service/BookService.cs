using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQLAgentApp.Mapper;
using GraphQLAgentApp.Models.Dtos;
using GraphQLAgentApp.Models.Entities;
using GraphQLAgentApp.Repository;

namespace GraphQLAgentApp.Service
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IMappingService _mappingService;

        public BookService(IBookRepository repository, IMappingService mappingService)
        {
            _repository = repository;
            _mappingService = mappingService;
        }

        public async Task<List<BookDto>> GetAllAsync()
        {
            var books = await _repository.GetAllAsync();
            return _mappingService.MapList<Book, BookDto>(books);
        }

        public async Task<BookDto?> GetByIdAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);
            if (book == null) return null;
            return _mappingService.Map<Book, BookDto>(book);
        }

        public async Task<BookDto> AddAsync(string title, string author)
        {
            var book = await _repository.AddAsync(title, author);
            return _mappingService.Map<Book, BookDto>(book);
        }
    }
}
