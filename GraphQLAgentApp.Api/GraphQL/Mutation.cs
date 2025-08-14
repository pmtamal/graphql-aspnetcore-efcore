using GraphQLAgentApp.Service;
using GraphQLAgentApp.Models.GraphQL;
using GraphQLAgentApp.Mapper;

namespace GraphQLAgentApp.Api.GraphQL
{
    public class Mutation(IBookService service, IMappingService mappingService, IUserService userService)
    {
        public async Task<BookGraphQLModel> AddBook(
            string title, 
            int authorId, 
            int categoryId, 
            string isbn, 
            string description, 
            int publicationYear, 
            string publisher, 
            int pages, 
            string language, 
            decimal price, 
            int stockQuantity)
        {
            var bookDto = await service.AddAsync(title, authorId, categoryId, isbn, description, publicationYear, publisher, pages, language, price, stockQuantity);
            return mappingService.Map<BookGraphQLModel>(bookDto);
        }

        public async Task<UserGraphQLModel?> Login(string username, string password)
        {
            var userDto = await userService.AuthenticateAsync(username, password);
            return userDto != null ? mappingService.Map<UserGraphQLModel>(userDto) : null;
        }

        public async Task<UserGraphQLModel> Register(
            string username, 
            string email, 
            string password, 
            string firstName, 
            string lastName)
        {
            var userDto = new GraphQLAgentApp.Models.Dtos.UserDto
            {
                Username = username,
                Email = email,
                PasswordHash = password, // Will be hashed in service
                IsAdmin = false,
                IsActive = true
            };

            var createdUser = await userService.CreateAsync(userDto);
            
            // Create user profile
            var userProfileDto = new GraphQLAgentApp.Models.Dtos.UserProfileDto
            {
                UserId = createdUser.Id,
                FirstName = firstName,
                LastName = lastName,
                CreatedAt = DateTime.UtcNow
            };
            
            // Note: We'll need to inject IUserProfileRepository to create the profile
            // For now, we'll return the user without profile
            return mappingService.Map<UserGraphQLModel>(createdUser);
        }
    }
}