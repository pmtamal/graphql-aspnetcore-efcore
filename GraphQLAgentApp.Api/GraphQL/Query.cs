using System.Linq;
using GraphQLAgentApp.Service;
using GraphQLAgentApp.Models.GraphQL;
using GraphQLAgentApp.Mapper;
using HotChocolate;
using HotChocolate.Data;

namespace GraphQLAgentApp.Api.GraphQL
{
    public class Query
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<BookGraphQLModel> GetBooks([Service] IBookService service, [Service] IMappingService mappingService)
        {
            return mappingService.ProjectTo<BookGraphQLModel>(service.GetAll());
        }

        public BookGraphQLModel? GetBookById(int id, [Service] IBookService service, [Service] IMappingService mappingService)
        {
            var bookDto = service.GetById(id);
            return bookDto == null ? null : mappingService.Map<BookGraphQLModel>(bookDto);
        }
    }
}