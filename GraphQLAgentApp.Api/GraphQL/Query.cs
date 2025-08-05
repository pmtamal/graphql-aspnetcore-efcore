using GraphQLAgentApp.Service;
using GraphQLAgentApp.Models.GraphQL;
using GraphQLAgentApp.Mapper;
using GraphQLAgentApp.Models.Dtos;

namespace GraphQLAgentApp.Api.GraphQL
{
    /// <summary>
    /// Root GraphQL query class for fetching book data.
    /// Provides endpoints for retrieving all books or a single book by ID.
    /// </summary>
    public class Query
    {
        private readonly IBookService _service;
        // Service for mapping between DTOs and GraphQL models
        private readonly IMappingService _mappingService;

        /// <summary>
        /// Constructor for Query class, injects required services.
        /// </summary>
        /// <param name="service">Book service</param>
        /// <param name="mappingService">Mapping service</param>
        public Query(IBookService service, IMappingService mappingService)
        {
            _service = service;
            _mappingService = mappingService;
        }

        /// <summary>
        /// Gets all books with support for projection, filtering, and sorting.
        /// </summary>
        /// <returns>List of BookGraphQLModel</returns>
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<List<BookGraphQLModel>> GetBooks()
        {
            var bookDtos = await _service.GetAllAsync();
            return _mappingService.MapList<BookDto, BookGraphQLModel>(bookDtos);
        }

        /// <summary>
        /// Gets a single book by its ID.
        /// </summary>
        /// <param name="id">Book ID</param>
        /// <returns>BookGraphQLModel or null if not found</returns>
        public async Task<BookGraphQLModel?> GetBookById(int id)
        {
            var bookDto = await _service.GetByIdAsync(id);
            return bookDto == null ? null : _mappingService.Map<BookGraphQLModel>(bookDto);
        }
    }
}