using AspNetRestApiContainer.Domain.Entities;
using AutoBogus;
using Bogus;
using System;

namespace AspNetRestApiContainer.Infrastructure.Shared.Mock
{
    public class ProductSeedBogusConfig : AutoFaker<Product>
    {
        public ProductSeedBogusConfig()
        {
            Randomizer.Seed = new Random(8675309);
            RuleFor(m => m.Id, f => Guid.NewGuid());
            RuleFor(o => o.Number, f => f.Commerce.Ean13());
            RuleFor(o => o.Price, f => f.Finance.Amount());
        }
    }
}