using GraphQLAgentApp.Mapper;
using GraphQLAgentApp.Models.Dtos;
using GraphQLAgentApp.Models.Entities;
using GraphQLAgentApp.Repository;

namespace GraphQLAgentApp.Service
{
    public class AuthorService(IAuthorRepository repository, IMappingService mappingService) : IAuthorService
    {
        public async Task<List<AuthorDto>> GetAllAsync()
        {
            var authors = await repository.GetAllAsync();
            return mappingService.MapList<Author, AuthorDto>(authors);
        }

        public async Task<AuthorDto?> GetByIdAsync(int id)
        {
            var author = await repository.GetByIdAsync(id);
            if (author == null) return null;
            return mappingService.Map<Author, AuthorDto>(author);
        }

        public async Task<AuthorDto> AddAsync(string firstName, string lastName, string? biography, DateTime? dateOfBirth, string? nationality, string? website)
        {
            var author = await repository.AddAsync(firstName, lastName, biography, dateOfBirth, nationality, website);
            return mappingService.Map<Author, AuthorDto>(author);
        }
    }
} 