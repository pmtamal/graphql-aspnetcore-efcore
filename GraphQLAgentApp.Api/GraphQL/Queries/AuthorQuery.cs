using GraphQLAgentApp.Service;
using GraphQLAgentApp.Models.GraphQL;
using GraphQLAgentApp.Mapper;
using GraphQLAgentApp.Models.Dtos;

namespace GraphQLAgentApp.Api.GraphQL.Queries
{
    /// <summary>
    /// GraphQL extension for Author operations
    /// </summary>
    [ExtendObjectType(typeof(Query))]
    public class AuthorQuery
    {
        private readonly IMappingService _mappingService;

        public AuthorQuery(IMappingService mappingService)
        {
            _mappingService = mappingService;
        }

        /// <summary>
        /// Gets all authors with support for projection, filtering, and sorting.
        /// </summary>
        /// <returns>List of AuthorGraphQLModel</returns>
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<List<AuthorGraphQLModel>> Authors([Service] IAuthorService service)
        {
            var authorDtos = await service.GetAllAsync();
            return _mappingService.MapList<AuthorDto, AuthorGraphQLModel>(authorDtos);
        }

        /// <summary>
        /// Gets a single author by their ID.
        /// </summary>
        /// <param name="id">Author ID</param>
        /// <returns>AuthorGraphQLModel or null if not found</returns>
        public async Task<AuthorGraphQLModel?> AuthorById([Service] IAuthorService service, int id)
        {
            var authorDto = await service.GetByIdAsync(id);
            return authorDto == null ? null : _mappingService.Map<AuthorGraphQLModel>(authorDto);
        }

        /// <summary>
        /// Gets authors by nationality
        /// </summary>
        /// <param name="nationality">Nationality to filter by</param>
        /// <returns>List of authors with the specified nationality</returns>
        public async Task<List<AuthorGraphQLModel>> AuthorsByNationality([Service] IAuthorService service, string nationality)
        {
            var authorDtos = await service.GetAllAsync();
            var filteredAuthors = authorDtos.Where(a => a.Nationality?.Equals(nationality, StringComparison.OrdinalIgnoreCase) == true).ToList();
            return _mappingService.MapList<AuthorDto, AuthorGraphQLModel>(filteredAuthors);
        }

        /// <summary>
        /// Gets authors with books
        /// </summary>
        /// <returns>List of authors who have written books</returns>
        public async Task<List<AuthorGraphQLModel>> AuthorsWithBooks([Service] IAuthorService service)
        {
            var authorDtos = await service.GetAllAsync();
            var authorsWithBooks = authorDtos.Where(a => a.Books?.Any() == true).ToList();
            return _mappingService.MapList<AuthorDto, AuthorGraphQLModel>(authorsWithBooks);
        }
    }
} 