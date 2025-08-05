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
    public class CategoryQuery(ICategoryService service, IMappingService mappingService)
    {
        /// <summary>
        /// Gets all categories with support for projection, filtering, and sorting.
        /// </summary>
        /// <returns>List of CategoryGraphQLModel</returns>
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<List<CategoryGraphQLModel>> GetCategories()
        {
            var categoryDtos = await service.GetAllAsync();
            return mappingService.MapList<CategoryDto, CategoryGraphQLModel>(categoryDtos);
        }

        /// <summary>
        /// Gets a single category by its ID.
        /// </summary>
        /// <param name="id">Category ID</param>
        /// <returns>CategoryGraphQLModel or null if not found</returns>
        public async Task<CategoryGraphQLModel?> GetCategoryById(int id)
        {
            var categoryDto = await service.GetByIdAsync(id);
            return categoryDto == null ? null : mappingService.Map<CategoryGraphQLModel>(categoryDto);
        }

        /// <summary>
        /// Gets categories with books
        /// </summary>
        /// <returns>List of categories that have books</returns>
        public async Task<List<CategoryGraphQLModel>> GetCategoriesWithBooks()
        {
            var categoryDtos = await service.GetAllAsync();
            var categoriesWithBooks = categoryDtos.Where(c => c.Books?.Any() == true).ToList();
            return mappingService.MapList<CategoryDto, CategoryGraphQLModel>(categoriesWithBooks);
        }
    }
} 