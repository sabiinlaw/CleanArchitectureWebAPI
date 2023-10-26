using AspNetRestApiContainer.Domain.Entities;
using AspNetRestApiContainer.Domain.Enums;
using AutoBogus;
using Bogus;
using System;

namespace AspNetRestApiContainer.Infrastructure.Shared.Mock
{
    public class UserSeedBogusConfig : AutoFaker<User>
    {
        public UserSeedBogusConfig()
        {
            Randomizer.Seed = new Random(8675309);
            RuleFor(p => p.Id, f => Guid.NewGuid());
            RuleFor(p => p.Login, f => f.Name.LastName().ToLower());
            RuleFor(p => p.Password, f => f.Random.AlphaNumeric(5));
            RuleFor(p => p.Title, f => f.PickRandom<UserTitle>());
        }
    }
}
