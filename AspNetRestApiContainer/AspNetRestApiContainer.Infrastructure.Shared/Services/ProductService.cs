using AspNetRestApiContainer.Application.Exceptions;
using AspNetRestApiContainer.Application.Interfaces;
using AspNetRestApiContainer.Application.Interfaces.Repositories;
using AspNetRestApiContainer.Application.Parameters.Commands;
using AspNetRestApiContainer.Application.Parameters.Queries;
using AspNetRestApiContainer.Application.Views;
using AspNetRestApiContainer.Application.Wrappers;
using AspNetRestApiContainer.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetRestApiContainer.Infrastructure.Shared.Services
{
    public interface IProductService
    {
        Task<PagedResponse<IEnumerable<Product>>> GetProducts(GetProductQuery request);
        Task<Response<Product>> GetById(Guid id);
        Task<Response<Guid>> CreateAsync(ProductCreate command);
        Task<Response<Guid>> UpdateAsync(Guid id, ProductUpdate command);
        Task<Response<Guid>> DeleteAsync(Guid id);
    }

    public class ProductService : IProductService
    {
        private readonly IProductRepositoryAsync _productRepository;
        private readonly IValidator<ProductCreate> _validator;
        private readonly IModelHelper _modelHelper;

        public ProductService(
            IProductRepositoryAsync productRepository,
            IValidator<ProductCreate> validator,
            IModelHelper modelHelper)
        {
            _productRepository = productRepository;
            _validator = validator;
            _modelHelper = modelHelper;
        }

        public async Task<PagedResponse<IEnumerable<Product>>> GetProducts(GetProductQuery request)
        {
            var validFilter = request;
            if (!string.IsNullOrEmpty(validFilter.Fields))
            {
                validFilter.Fields = _modelHelper.ValidateModelFields<ProductView>(validFilter.Fields);
            }
            if (string.IsNullOrEmpty(validFilter.Fields))
            {
                validFilter.Fields = _modelHelper.GetModelFields<ProductView>();
            }

            var entitys = await _productRepository.GetPagedProductReponseAsync(validFilter);
            var data = entitys.data;
            var recordCount = entitys.recordsCount;

            return new PagedResponse<IEnumerable<Product>>(data, validFilter.PageNumber, validFilter.PageSize, recordCount);
        }

        public async Task<Response<Product>> GetById(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new ApiException($"Product Not Found.");

            return new Response<Product>(product);
        }

        public async Task<Response<Guid>> CreateAsync(ProductCreate command)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return new Response<Guid> { Errors = errors };
            }

            var product = new Product()
            {
                Price = command.Price,
                Number = command.Number
            };

            await _productRepository.AddAsync(product);
            return new Response<Guid>(product.Id);
        }

        public async Task<Response<Guid>> UpdateAsync(Guid id, ProductUpdate command)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                throw new ApiException($"Product Not Found.");
            }
            else
            {
                product.Number = command.Number;
                product.Price = command.Price;

                await _productRepository.UpdateAsync(product);
                return new Response<Guid>(product.Id);
            }
        }

        public async Task<Response<Guid>> DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new ApiException($"Product Not Found.");

            await _productRepository.DeleteAsync(product);
            return new Response<Guid>(product.Id);
        }
    }
}
