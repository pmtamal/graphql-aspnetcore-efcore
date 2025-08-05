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
    public class AuthorQuery(IAuthorService service, IMappingService mappingService)
    {
        /// <summary>
        /// Gets all authors with support for projection, filtering, and sorting.
        /// </summary>
        /// <returns>List of AuthorGraphQLModel</returns>
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<List<AuthorGraphQLModel>> GetAuthors()
        {
            var authorDtos = await service.GetAllAsync();
            return mappingService.MapList<AuthorDto, AuthorGraphQLModel>(authorDtos);
        }

        /// <summary>
        /// Gets a single author by their ID.
        /// </summary>
        /// <param name="id">Author ID</param>
        /// <returns>AuthorGraphQLModel or null if not found</returns>
        public async Task<AuthorGraphQLModel?> GetAuthorById(int id)
        {
            var authorDto = await service.GetByIdAsync(id);
            return authorDto == null ? null : mappingService.Map<AuthorGraphQLModel>(authorDto);
        }

        /// <summary>
        /// Gets authors by nationality
        /// </summary>
        /// <param name="nationality">Nationality to filter by</param>
        /// <returns>List of authors with the specified nationality</returns>
        public async Task<List<AuthorGraphQLModel>> GetAuthorsByNationality(string nationality)
        {
            var authorDtos = await service.GetAllAsync();
            var filteredAuthors = authorDtos.Where(a => a.Nationality?.Equals(nationality, StringComparison.OrdinalIgnoreCase) == true).ToList();
            return mappingService.MapList<AuthorDto, AuthorGraphQLModel>(filteredAuthors);
        }

        /// <summary>
        /// Gets authors with books
        /// </summary>
        /// <returns>List of authors who have written books</returns>
        public async Task<List<AuthorGraphQLModel>> GetAuthorsWithBooks()
        {
            var authorDtos = await service.GetAllAsync();
            var authorsWithBooks = authorDtos.Where(a => a.Books?.Any() == true).ToList();
            return mappingService.MapList<AuthorDto, AuthorGraphQLModel>(authorsWithBooks);
        }
    }
} 