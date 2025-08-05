namespace GraphQLAgentApp.Mapper
{
    /// <summary>
    /// Interface for mapping service that wraps AutoMapper functionality
    /// </summary>
    public interface IMappingService
    {
        TDestination Map<TSource, TDestination>(TSource source);
        TDestination Map<TDestination>(object source);
        List<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source);
        IQueryable<TDestination> ProjectTo<TDestination>(IQueryable source);
    }
}