using AspNetRestApiContainer.Application.Wrappers;
using AspNetRestApiContainer.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using AspNetRestApiContainer.Application.Interfaces.Repositories;
using AspNetRestApiContainer.Application.Exceptions;

namespace AspNetRestApiContainer.Infrastructure.Shared.Services
{
    public interface IUserService
    {
        Task<Response<IEnumerable<User>>> GetAllUsers();
        Task<User> GeByUserName(string username);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepositoryAsync _userRepository;

        public UserService(
            IUserRepositoryAsync userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Response<IEnumerable<User>>> GetAllUsers()
        {
            var users = await _userRepository.GetUserReponseAsync();
            return new Response<IEnumerable<User>>(users);
        }

        public async Task<User> GeByUserName(string username)
        {
            var user = await _userRepository.GetByUserNameAsync(username);
            if (user == null)
                throw new ApiException($"User Not Found.");

            return user;
        }
    }
}
