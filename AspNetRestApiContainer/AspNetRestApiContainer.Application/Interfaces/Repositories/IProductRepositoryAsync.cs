using AspNetRestApiContainer.Application.Parameters;
using AspNetRestApiContainer.Application.Parameters.Queries;
using AspNetRestApiContainer.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetRestApiContainer.Application.Interfaces.Repositories
{
    public interface IProductRepositoryAsync : IGenericRepositoryAsync<Product>
    {
        Task<bool> IsUniqueProductNumberAsync(string productNumber);

        Task<(IEnumerable<Product> data, RecordsCount recordsCount)> GetPagedProductReponseAsync(GetProductQuery requestParameters);
    }
}