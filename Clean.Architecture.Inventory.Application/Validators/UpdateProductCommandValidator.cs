using Clean.Architecture.Inventory.Application.Commands;
using Clean.Architecture.Inventory.Application.Interfaces;
using FluentValidation;

namespace Clean.Architecture.Inventory.Application.Validators
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
	{
		private readonly ICategoryProductRepository _categoryRepository;

		public UpdateProductCommandValidator(ICategoryProductRepository categoryProductRepository)
		{
			RuleFor(x => x.Id)
				.GreaterThan(0).WithMessage("Invalid product ID.");

			RuleFor(x => x.Name)
			   .NotEmpty().WithMessage("Name is required.")
			   .MaximumLength(50);

			RuleFor(x => x.Description)
				.NotEmpty().WithMessage("Description is required.")
				.MaximumLength(100);

			RuleFor(x => x.Price)
				.GreaterThanOrEqualTo(0).WithMessage("Average pirce must be non-negative.");

			RuleFor(x => x.ExpirationDate)
					.Must(date => date <= DateTime.Now.Date)
					.WithMessage("The expiration date must be greater than or equal to the current date.");
			// Validação do CategoryId
			RuleFor(x => x.CategoryId)
				.MustAsync(async (categoryId, cancellationToken) =>
					await CategoryExists(categoryId))
				.WithMessage("The specified CategoryId does not exist.");
		}

		private async Task<bool> CategoryExists(int categoryId)
		{
			// Verifica se a categoria existe no banco de dados
			return await _categoryRepository.ExistsAsync(categoryId);
		}
	}
}
