using GraphQLAgentApp.Models.Entities;

namespace GraphQLAgentApp.Repository
{
    public interface IAuthorRepository
    {
        Task<List<Author>> GetAllAsync();
        Task<Author?> GetByIdAsync(int id);
        Task<Author> AddAsync(string firstName, string lastName, string? biography, DateTime? dateOfBirth, string? nationality, string? website);
    }
} 