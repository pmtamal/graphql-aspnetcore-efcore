using GraphQLAgentApp.Models.Dtos;

namespace GraphQLAgentApp.Service
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync();
        Task<CategoryDto?> GetByIdAsync(int id);
        Task<CategoryDto> AddAsync(string name, string? description);
    }
} 