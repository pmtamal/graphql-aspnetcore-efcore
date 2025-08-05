using GraphQLAgentApp.Models.Dtos;

namespace GraphQLAgentApp.Service
{
    public interface IBookService
    {
        Task<List<BookDto>> GetAllAsync();
        Task<BookDto?> GetByIdAsync(int id);
        Task<BookDto> AddAsync(string title, string author);
    }
}
