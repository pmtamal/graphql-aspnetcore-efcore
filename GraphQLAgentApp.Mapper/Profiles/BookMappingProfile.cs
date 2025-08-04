using AutoMapper;
using GraphQLAgentApp.Models.Entities;
using GraphQLAgentApp.Models.Dtos;
using GraphQLAgentApp.Models.GraphQL;

namespace GraphQLAgentApp.Mapper
{
    /// <summary>
    /// AutoMapper profile for Book entity mappings between all layers
    /// </summary>
    public class BookMappingProfile : Profile
    {
        public BookMappingProfile()
        {
            // Entity to DTO mapping (Repository ? Service layer)
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt));

            // DTO to Entity mapping (Service ? Repository layer)
            CreateMap<BookDto, Book>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Condition(src => src.CreatedAt.HasValue));

            // DTO to GraphQL Model mapping (Service ? GraphQL layer)
            CreateMap<BookDto, BookGraphQLModel>();

            // GraphQL Model to DTO mapping (GraphQL ? Service layer for mutations)
            CreateMap<BookGraphQLModel, BookDto>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());

            // Entity to GraphQL Model mapping (direct mapping when needed)
            CreateMap<Book, BookGraphQLModel>();

            // IQueryable mapping configurations for projections
            CreateMap<IQueryable<Book>, IQueryable<BookDto>>();
            CreateMap<IQueryable<BookDto>, IQueryable<BookGraphQLModel>>();
        }
    }



    /// <summary>
    /// Implementation of mapping service using AutoMapper
    /// </summary>
    public class MappingService : IMappingService
    {
        private readonly IMapper _mapper;

        public MappingService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return _mapper.Map<TSource, TDestination>(source);
        }

        public TDestination Map<TDestination>(object source)
        {
            return _mapper.Map<TDestination>(source);
        }

        public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source)
        {
            return _mapper.ProjectTo<TDestination>(source);
        }

        public List<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source)
        {
            return _mapper.Map<List<TDestination>>(source);
        }
    }
}