using GraphQLAgentApp.Service;
using GraphQLAgentApp.Models.GraphQL;
using GraphQLAgentApp.Mapper;

namespace GraphQLAgentApp.Api.GraphQL
{
    public class Mutation(IBookService service, IMappingService mappingService)
    {
        public async Task<BookGraphQLModel> AddBook(string title, string author)
        {
            var bookDto = await service.AddAsync(title, author);
            return mappingService.Map<BookGraphQLModel>(bookDto);
        }
    }
}