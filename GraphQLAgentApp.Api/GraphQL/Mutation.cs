using GraphQLAgentApp.Service;
using GraphQLAgentApp.Models.GraphQL;
using GraphQLAgentApp.Mapper;

namespace GraphQLAgentApp.Api.GraphQL
{
    public class Mutation(IBookService service, IMappingService mappingService)
    {
        public async Task<BookGraphQLModel> AddBook(
            string title, 
            int authorId, 
            int categoryId, 
            string isbn, 
            string description, 
            int publicationYear, 
            string publisher, 
            int pages, 
            string language, 
            decimal price, 
            int stockQuantity)
        {
            var bookDto = await service.AddAsync(title, authorId, categoryId, isbn, description, publicationYear, publisher, pages, language, price, stockQuantity);
            return mappingService.Map<BookGraphQLModel>(bookDto);
        }
    }
}