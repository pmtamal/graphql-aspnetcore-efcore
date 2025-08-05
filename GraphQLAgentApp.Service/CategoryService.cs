using GraphQLAgentApp.Mapper;
using GraphQLAgentApp.Models.Dtos;
using GraphQLAgentApp.Models.Entities;
using GraphQLAgentApp.Repository;

namespace GraphQLAgentApp.Service
{
    public class CategoryService(ICategoryRepository repository, IMappingService mappingService) : ICategoryService
    {
        public async Task<List<CategoryDto>> GetAllAsync()
        {
            var categories = await repository.GetAllAsync();
            return mappingService.MapList<Category, CategoryDto>(categories);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await repository.GetByIdAsync(id);
            if (category == null) return null;
            return mappingService.Map<Category, CategoryDto>(category);
        }

        public async Task<CategoryDto> AddAsync(string name, string? description)
        {
            var category = await repository.AddAsync(name, description);
            return mappingService.Map<Category, CategoryDto>(category);
        }
    }
} 