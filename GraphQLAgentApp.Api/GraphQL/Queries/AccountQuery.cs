using GraphQLAgentApp.Service;
using GraphQLAgentApp.Models.GraphQL;
using GraphQLAgentApp.Mapper;
using HotChocolate;

namespace GraphQLAgentApp.Api.GraphQL.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class AccountQuery
    {
        /// <summary>
        /// Authenticate user login
        /// </summary>
        /// <param name="userService">User service injected by HotChocolate</param>
        /// <param name="mappingService">Mapping service injected by HotChocolate</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>User information if authentication successful</returns>
        public async Task<UserGraphQLModel?> Login(
            [Service] IUserService userService,
            [Service] IMappingService mappingService,
            string username, 
            string password)
        {
            var userDto = await userService.AuthenticateAsync(username, password);
            return userDto != null ? mappingService.Map<UserGraphQLModel>(userDto) : null;
        }

        /// <summary>
        /// Validate user credentials without logging in
        /// </summary>
        /// <param name="userService">User service injected by HotChocolate</param>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>True if credentials are valid</returns>
        public async Task<bool> ValidateCredentials(
            [Service] IUserService userService,
            string username, 
            string password)
        {
            return await userService.ValidateCredentialsAsync(username, password);
        }

        /// <summary>
        /// Logout user (server-side logout)
        /// </summary>
        /// <param name="userService">User service injected by HotChocolate</param>
        /// <param name="userId">User ID to logout</param>
        /// <returns>Success status of logout operation</returns>
        public async Task<bool> Logout(
            [Service] IUserService userService,
            int userId)
        {
            return await userService.LogoutAsync(userId);
        }
    }
}
