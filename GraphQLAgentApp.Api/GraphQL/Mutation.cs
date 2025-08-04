using GraphQLAgentApp.Service;
using GraphQLAgentApp.Models.GraphQL;
using GraphQLAgentApp.Mapper;
using HotChocolate;

namespace GraphQLAgentApp.Api.GraphQL
{
    public class Mutation
    {
        public BookGraphQLModel AddBook(string title, string author, [Service] IBookService service, [Service] IMappingService mappingService)
        {
            var bookDto = service.Add(title, author);
            return mappingService.Map<BookGraphQLModel>(bookDto);
        }
    }
}