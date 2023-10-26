using AspNetRestApiContainer.Application.Interfaces;
using AspNetRestApiContainer.Domain.Entities;
using AspNetRestApiContainer.Infrastructure.Shared.Mock;
using System.Collections.Generic;

namespace AspNetRestApiContainer.Infrastructure.Shared.Services
{
    public class MockService : IMockService
    {
        public List<User> SeedUsers(int rowCount)
        {
            var seedFaker = new UserSeedBogusConfig();
            return seedFaker.Generate(rowCount);
        }

        public List<Product> SeedProducts(int rowCount)
        {
            var seedFaker = new ProductSeedBogusConfig();
            return seedFaker.Generate(rowCount);
        }
    }
}