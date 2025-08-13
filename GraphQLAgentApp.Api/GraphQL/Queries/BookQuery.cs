using GraphQLAgentApp.Service;
using GraphQLAgentApp.Models.GraphQL;
using GraphQLAgentApp.Mapper;
using GraphQLAgentApp.Models.Dtos;

namespace GraphQLAgentApp.Api.GraphQL.Queries
{
    /// <summary>
    /// GraphQL extension for Book operations
    /// </summary>
    [ExtendObjectType(typeof(Query))]
    public class BookQuery
    {
        private readonly IMappingService _mappingService;

        public BookQuery(IMappingService mappingService)
        {
            _mappingService = mappingService;
        }

        /// <summary>
        /// Gets all books with support for projection, filtering, and sorting.
        /// </summary>
        /// <returns>List of BookGraphQLModel</returns>
        [UseFiltering]
        [UseSorting]
        public async Task<List<BookGraphQLModel>> Books([Service] IBookService service)
        {
            var bookDtos = await service.GetAllAsync();
            return _mappingService.MapList<BookDto, BookGraphQLModel>(bookDtos);
        }

        /// <summary>
        /// Gets a single book by its ID.
        /// </summary>
        /// <param name="id">Book ID</param>
        /// <returns>BookGraphQLModel or null if not found</returns>
        public async Task<BookGraphQLModel?> BookById([Service] IBookService service, int id)
        {
            var bookDto = await service.GetByIdAsync(id);
            return bookDto == null ? null : _mappingService.Map<BookGraphQLModel>(bookDto);
        }

        /// <summary>
        /// Gets books by author ID
        /// </summary>
        /// <param name="authorId">Author ID</param>
        /// <returns>List of books by the specified author</returns>
        public async Task<List<BookGraphQLModel>> BooksByAuthor([Service] IBookService service, int authorId)
        {
            var bookDtos = await service.GetAllAsync();
            var filteredBooks = bookDtos.Where(b => b.AuthorId == authorId).ToList();
            return _mappingService.MapList<BookDto, BookGraphQLModel>(filteredBooks);
        }

        /// <summary>
        /// Gets books by category ID
        /// </summary>
        /// <param name="categoryId">Category ID</param>
        /// <returns>List of books in the specified category</returns>
        public async Task<List<BookGraphQLModel>> BooksByCategory([Service] IBookService service, int categoryId)
        {
            var bookDtos = await service.GetAllAsync();
            var filteredBooks = bookDtos.Where(b => b.CategoryId == categoryId).ToList();
            return _mappingService.MapList<BookDto, BookGraphQLModel>(filteredBooks);
        }

        /// <summary>
        /// Gets books that are in stock
        /// </summary>
        /// <returns>List of books that are available</returns>
        public async Task<List<BookGraphQLModel>> AvailableBooks([Service] IBookService service)
        {
            var bookDtos = await service.GetAllAsync();
            var availableBooks = bookDtos.Where(b => b.IsAvailable && b.StockQuantity > 0).ToList();
            return _mappingService.MapList<BookDto, BookGraphQLModel>(availableBooks);
        }
    }
} 