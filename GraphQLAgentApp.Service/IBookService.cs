using GraphQLAgentApp.Models.Dtos;

namespace GraphQLAgentApp.Service
{
    public interface IBookService
    {
        IQueryable<BookDto> GetAll();
        BookDto? GetById(int id);
        BookDto Add(string title, string author);
    }
}
