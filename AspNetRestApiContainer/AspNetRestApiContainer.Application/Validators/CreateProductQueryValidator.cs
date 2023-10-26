using AspNetRestApiContainer.Application.Interfaces.Repositories;
using AspNetRestApiContainer.Application.Parameters.Commands;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetRestApiContainer.Application.Validators
{
    public class CreateProductQueryValidator : AbstractValidator<ProductCreate>
    {
        private readonly IProductRepositoryAsync productRepository;

        public CreateProductQueryValidator(IProductRepositoryAsync productRepository)
        {
            this.productRepository = productRepository;

            RuleFor(p => p.Number)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
                .MustAsync(IsUniqueNumber).WithMessage("{PropertyName} already exists.");

            RuleFor(p => p.Price)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThan(0)
                .WithMessage("{PropertyName} must be greater then 0.");
        }

        private async Task<bool> IsUniqueNumber(string productNumber, CancellationToken cancellationToken)
        {
            return await productRepository.IsUniqueProductNumberAsync(productNumber);
        }
    }
}