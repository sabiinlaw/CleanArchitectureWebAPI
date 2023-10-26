using AspNetRestApiContainer.Domain.Enums;
using System;

namespace AspNetRestApiContainer.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserTitle Title { get; set; }
    }
}
