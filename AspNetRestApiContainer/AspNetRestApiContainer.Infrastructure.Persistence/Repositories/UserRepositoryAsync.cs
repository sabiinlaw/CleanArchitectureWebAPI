using AspNetRestApiContainer.Application.Interfaces.Repositories;
using AspNetRestApiContainer.Domain.Entities;
using AspNetRestApiContainer.Infrastructure.Persistence.Contexts;
using AspNetRestApiContainer.Infrastructure.Persistence.Repository;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetRestApiContainer.Infrastructure.Persistence.Repositories
{
    public class UserRepositoryAsync : GenericRepositoryAsync<User>, IUserRepositoryAsync
    {
        private readonly DbSet<User> _users;

        public UserRepositoryAsync(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _users = dbContext.Set<User>();
        }

        public async Task<IEnumerable<User>> GetUserReponseAsync()
        {
            var result = _users
                .AsNoTracking()
                .AsExpandable();

            var resultData = await result.ToListAsync();
            return resultData;
        }

        public async Task<User> GetByUserNameAsync(string username)
        {
            return await _users.FirstAsync(x => x.Login == username);
        }
    }
}
