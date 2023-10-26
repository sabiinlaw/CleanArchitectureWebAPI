using AspNetRestApiContainer.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetRestApiContainer.Application.Interfaces.Repositories
{
    public interface IUserRepositoryAsync
    {
        Task<IEnumerable<User>> GetUserReponseAsync();
        Task<User> GetByUserNameAsync(string username);
    }
}
