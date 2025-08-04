using System.Linq;
using GraphQLAgentApp.Models.Entities;


namespace GraphQLAgentApp.Repository
{
    public interface IBookRepository
    {
        IQueryable<Book> GetAll();
        Book? GetById(int id);
        Book Add(string title, string author);
    }
}
