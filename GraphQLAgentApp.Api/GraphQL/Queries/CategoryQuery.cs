using GraphQLAgentApp.Service;
using GraphQLAgentApp.Models.GraphQL;
using GraphQLAgentApp.Mapper;
using GraphQLAgentApp.Models.Dtos;

namespace GraphQLAgentApp.Api.GraphQL.Queries
{
    /// <summary>
    /// GraphQL extension for Category operations
    /// </summary>
    [ExtendObjectType(typeof(Query))]
    public class CategoryQuery
    {
        private readonly IMappingService _mappingService;

        public CategoryQuery(IMappingService mappingService)
        {
            _mappingService = mappingService;
        }

        /// <summary>
        /// Gets all categories with support for projection, filtering, and sorting.
        /// </summary>
        /// <returns>List of CategoryGraphQLModel</returns>
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<List<CategoryGraphQLModel>> Categories([Service] ICategoryService service)
        {
            var categoryDtos = await service.GetAllAsync();
            return _mappingService.MapList<CategoryDto, CategoryGraphQLModel>(categoryDtos);
        }

        /// <summary>
        /// Gets a single category by its ID.
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>CategoryGraphQLModel or null if not found</returns>
        public async Task<CategoryGraphQLModel?> CategoryById([Service] ICategoryService service, int id)
        {
            var categoryDto = await service.GetByIdAsync(id);
            return categoryDto == null ? null : _mappingService.Map<CategoryGraphQLModel>(categoryDto);
        }

        /// <summary>
        /// Gets categories with books
        /// </summary>
        /// <returns>List of categories that have books</returns>
        public async Task<List<CategoryGraphQLModel>> CategoriesWithBooks([Service] ICategoryService service)
        {
            var categoryDtos = await service.GetAllAsync();
            var categoriesWithBooks = categoryDtos.Where(c => c.Books?.Any() == true).ToList();
            return _mappingService.MapList<CategoryDto, CategoryGraphQLModel>(categoriesWithBooks);
        }
    }
} 