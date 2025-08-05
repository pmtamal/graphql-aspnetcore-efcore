using GraphQLAgentApp.Models.Entities;

namespace GraphQLAgentApp.Repository
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<Category> AddAsync(string name, string? description);
    }
} 