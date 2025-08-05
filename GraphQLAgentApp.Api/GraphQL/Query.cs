using GraphQLAgentApp.Api.GraphQL.Queries;

namespace GraphQLAgentApp.Api.GraphQL
{
    /// <summary>
    /// Root GraphQL query class that serves as a base for extensions.
    /// </summary>
    public class Query
    {
        /// <summary>
        /// Simple health check field to satisfy GraphQL requirements
        /// </summary>
        /// <returns>Health status</returns>
        public string HealthCheck() => "GraphQL API is running!";
    }
}