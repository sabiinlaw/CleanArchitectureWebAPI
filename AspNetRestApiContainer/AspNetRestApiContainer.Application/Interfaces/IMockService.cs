using AspNetRestApiContainer.Domain.Entities;
using System.Collections.Generic;

namespace AspNetRestApiContainer.Application.Interfaces
{
    public interface IMockService
    {
        List<Product> SeedProducts(int rowCount);
        List<User> SeedUsers(int rowCount);      
    }
}