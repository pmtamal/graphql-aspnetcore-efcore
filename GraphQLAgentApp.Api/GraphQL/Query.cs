using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQLAgentApp.Service;
using GraphQLAgentApp.Models.GraphQL;
using GraphQLAgentApp.Mapper;
using GraphQLAgentApp.Models.Dtos;

namespace GraphQLAgentApp.Api.GraphQL
{
    public class Query
    {
        private readonly IBookService _service;
        private readonly IMappingService _mappingService;

        public Query(IBookService service, IMappingService mappingService)
        {
            _service = service;
            _mappingService = mappingService;
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<List<BookGraphQLModel>> GetBooks()
        {
            var bookDtos = await _service.GetAllAsync();
            return _mappingService.MapList<BookDto, BookGraphQLModel>(bookDtos);
        }

        public async Task<BookGraphQLModel?> GetBookById(int id)
        {
            var bookDto = await _service.GetByIdAsync(id);
            return bookDto == null ? null : _mappingService.Map<BookGraphQLModel>(bookDto);
        }
    }
}