using AspNetRestApiContainer.Application.Interfaces.Repositories;
using AspNetRestApiContainer.Application.Parameters;
using AspNetRestApiContainer.Application.Parameters.Queries;
using AspNetRestApiContainer.Domain.Entities;
using AspNetRestApiContainer.Infrastructure.Persistence.Contexts;
using AspNetRestApiContainer.Infrastructure.Persistence.Repository;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace AspNetRestApiContainer.Infrastructure.Persistence.Repositories
{
    public class ProductRepositoryAsync : GenericRepositoryAsync<Product>, IProductRepositoryAsync
    {
        private readonly DbSet<Product> _products;

        public ProductRepositoryAsync(ApplicationDbContext dbContext)
            : base(dbContext)
        {
            _products = dbContext.Set<Product>();
        }

        public async Task<bool> IsUniqueProductNumberAsync(string productNumber)
        {
            return await _products
                .AllAsync(p => p.Number != productNumber);
        }

        public async Task<(IEnumerable<Product> data, RecordsCount recordsCount)> GetPagedProductReponseAsync(GetProductQuery requestParameter)
        {
            var productNumber = requestParameter.Number;
            var productPrice = requestParameter.Price;

            var pageNumber = requestParameter.PageNumber;
            var pageSize = requestParameter.PageSize;
            var orderBy = requestParameter.OrderBy;
            var fields = requestParameter.Fields;

            int recordsTotal, recordsFiltered;

            var result = _products.AsNoTracking().AsExpandable();

            recordsTotal = await result.CountAsync();

            FilterByColumn(ref result, productNumber, productPrice);

            recordsFiltered = await result.CountAsync();

            var recordsCount = new RecordsCount
            {
                RecordsFiltered = recordsFiltered,
                RecordsTotal = recordsTotal
            };

            if (!string.IsNullOrWhiteSpace(orderBy))
                result = result.OrderBy(orderBy);

            if (!string.IsNullOrWhiteSpace(fields))
                result = result.Select<Product>("new(" + fields + ")");

            result = result.Skip((pageNumber - 1) * pageSize).Take(pageSize);
            var resultData = await result.ToListAsync();

            return (resultData, recordsCount);
        }

        private static void FilterByColumn(ref IQueryable<Product> products, string productNumber, decimal productPrice)
        {
            if (!products.Any())
                return;

            if (productPrice <= 0 && string.IsNullOrEmpty(productNumber))
                return;

            var predicate = PredicateBuilder.New<Product>();

            if (!string.IsNullOrEmpty(productNumber))
                predicate = predicate.And(p => p.Number.Contains(productNumber.Trim()));

            if (productPrice > 0)
                predicate = predicate.And(p => p.Price.ToString().Contains(productPrice.ToString()));

            products = products.Where(predicate);
        }
    }
}