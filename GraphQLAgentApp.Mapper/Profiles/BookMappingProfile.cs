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

            // Author mappings
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();
            CreateMap<AuthorDto, AuthorGraphQLModel>();
            CreateMap<AuthorGraphQLModel, AuthorDto>();
            CreateMap<Author, AuthorGraphQLModel>();

            // Category mappings
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
            CreateMap<CategoryDto, CategoryGraphQLModel>();
            CreateMap<CategoryGraphQLModel, CategoryDto>();
            CreateMap<Category, CategoryGraphQLModel>();

            // User mappings
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<UserDto, UserGraphQLModel>();
            CreateMap<UserGraphQLModel, UserDto>();
            CreateMap<User, UserGraphQLModel>();

            // UserProfile mappings
            CreateMap<UserProfile, UserProfileDto>();
            CreateMap<UserProfileDto, UserProfile>();
            CreateMap<UserProfileDto, UserProfileGraphQLModel>();
            CreateMap<UserProfileGraphQLModel, UserProfileDto>();
            CreateMap<UserProfile, UserProfileGraphQLModel>();

            // Order mappings
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<OrderDto, OrderGraphQLModel>();
            CreateMap<OrderGraphQLModel, OrderDto>();
            CreateMap<Order, OrderGraphQLModel>();

            // OrderItem mappings
            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<OrderItemDto, OrderItem>();
            CreateMap<OrderItemDto, OrderItemGraphQLModel>();
            CreateMap<OrderItemGraphQLModel, OrderItemDto>();
            CreateMap<OrderItem, OrderItemGraphQLModel>();

            // Review mappings
            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
            CreateMap<ReviewDto, ReviewGraphQLModel>();
            CreateMap<ReviewGraphQLModel, ReviewDto>();
            CreateMap<Review, ReviewGraphQLModel>();

            // IQueryable mapping configurations for projections
            CreateMap<IQueryable<Book>, IQueryable<BookDto>>();
            CreateMap<IQueryable<BookDto>, IQueryable<BookGraphQLModel>>();
        }
    }

    /// <summary>
    /// Implementation of mapping service using AutoMapper
    /// </summary>
    public class MappingService(IMapper mapper) : IMappingService
    {
        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return mapper.Map<TSource, TDestination>(source);
        }

        public TDestination Map<TDestination>(object source)
        {
            return mapper.Map<TDestination>(source);
        }

        public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source)
        {
            return mapper.ProjectTo<TDestination>(source);
        }

        public List<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source)
        {
            return mapper.Map<List<TDestination>>(source);
        }
    }
}