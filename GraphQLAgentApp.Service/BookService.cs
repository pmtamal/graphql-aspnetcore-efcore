using GraphQLAgentApp.Models.Dtos;
using GraphQLAgentApp.Models.Entities;
using GraphQLAgentApp.Repository;
using GraphQLAgentApp.Mapper;

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

        public IQueryable<BookDto> GetAll()
        {
            return _mappingService.ProjectTo<BookDto>(_repository.GetAll());
        }

        public BookDto? GetById(int id)
        {
            var book = _repository.GetById(id);
            if (book == null) return null;
            
            return _mappingService.Map<Book, BookDto>(book);
        }

        public BookDto Add(string title, string author)
        {
            var book = _repository.Add(title, author);
            return _mappingService.Map<Book, BookDto>(book);
        }
    }
}
