using GraphQLAgentApp.Models.Dtos;

namespace GraphQLAgentApp.Service
{
    public interface IAuthorService
    {
        Task<List<AuthorDto>> GetAllAsync();
        Task<AuthorDto?> GetByIdAsync(int id);
        Task<AuthorDto> AddAsync(string firstName, string lastName, string? biography, DateTime? dateOfBirth, string? nationality, string? website);
    }
} 