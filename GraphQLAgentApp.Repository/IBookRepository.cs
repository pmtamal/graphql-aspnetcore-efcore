using GraphQLAgentApp.Models.Entities;


namespace GraphQLAgentApp.Repository
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book> AddAsync(string title, string author);
    }
}
