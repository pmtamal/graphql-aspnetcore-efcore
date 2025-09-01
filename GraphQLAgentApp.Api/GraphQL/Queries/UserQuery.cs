using GraphQLAgentApp.Service;
using GraphQLAgentApp.Models.GraphQL;
using GraphQLAgentApp.Mapper;
using HotChocolate;

namespace GraphQLAgentApp.Api.GraphQL.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class UserQuery
    {
        /// <summary>
        /// Get all users (admin only)
        /// </summary>
        /// <param name="userService">User service injected by HotChocolate</param>
        /// <param name="mappingService">Mapping service injected by HotChocolate</param>
        /// <returns>List of all users</returns>
        public async Task<List<UserGraphQLModel>> GetUsers(
            [Service] IUserService userService,
            [Service] IMappingService mappingService)
        {
            var userDtos = await userService.GetAllAsync();
            return mappingService.Map<List<UserGraphQLModel>>(userDtos);
        }

        /// <summary>
        /// Get user by ID
        /// </summary>
        /// <param name="userService">User service injected by HotChocolate</param>
        /// <param name="mappingService">Mapping service injected by HotChocolate</param>
        /// <param name="id">User ID</param>
        /// <returns>User information</returns>
        public async Task<UserGraphQLModel?> GetUserById(
            [Service] IUserService userService,
            [Service] IMappingService mappingService,
            int id)
        {
            var userDto = await userService.GetByIdAsync(id);
            return userDto != null ? mappingService.Map<UserGraphQLModel>(userDto) : null;
        }

        /// <summary>
        /// Get user by username
        /// </summary>
        /// <param name="userService">User service injected by HotChocolate</param>
        /// <param name="mappingService">Mapping service injected by HotChocolate</param>
        /// <param name="username">Username</param>
        /// <returns>User information</returns>
        public async Task<UserGraphQLModel?> GetUserByUsername(
            [Service] IUserService userService,
            [Service] IMappingService mappingService,
            string username)
        {
            var userDto = await userService.GetByUsernameAsync(username);
            return userDto != null ? mappingService.Map<UserGraphQLModel>(userDto) : null;
        }

        /// <summary>
        /// Check if user exists
        /// </summary>
        /// <param name="userService">User service injected by HotChocolate</param>
        /// <param name="username">Username</param>
        /// <param name="email">Email</param>
        /// <returns>True if user exists</returns>
        public async Task<bool> UserExists(
            [Service] IUserService userService,
            string username, 
            string email)
        {
            return await userService.ExistsAsync(username, email);
        }

        /// <summary>
        /// Check if user is admin
        /// </summary>
        /// <param name="userService">User service injected by HotChocolate</param>
        /// <param name="userId">User ID</param>
        /// <returns>True if user is admin</returns>
        public async Task<bool> IsUserAdmin(
            [Service] IUserService userService,
            int userId)
        {
            return await userService.IsAdminAsync(userId);
        }
    }
}
