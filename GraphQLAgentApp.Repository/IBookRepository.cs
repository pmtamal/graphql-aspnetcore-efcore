using GraphQLAgentApp.Models.Entities;


namespace GraphQLAgentApp.Repository
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book> AddAsync(string title, int authorId, int categoryId, string isbn, string description, int publicationYear, string publisher, int pages, string language, decimal price, int stockQuantity);
    }
}
