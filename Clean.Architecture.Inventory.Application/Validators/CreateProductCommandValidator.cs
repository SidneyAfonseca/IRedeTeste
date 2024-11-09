using Clean.Architecture.Inventory.Application.Commands;
using Clean.Architecture.Inventory.Application.Interfaces;
using FluentValidation;

namespace Clean.Architecture.Inventory.Application.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
		private readonly ICategoryProductRepository _categoryRepository;

		public CreateProductCommandValidator(ICategoryProductRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;

			RuleFor(x => x.Name)
				.NotEmpty().WithMessage("Name is required.")
				.MaximumLength(50);

			RuleFor(x => x.Descritpion)
			.NotEmpty().WithMessage("Description is required.")
			.MaximumLength(50);

			RuleFor(x => x.Photo)
				.NotEmpty().WithMessage("Photo is required.")
				.MaximumLength(2048);

			RuleFor(x => x.Price)
				.GreaterThanOrEqualTo(0).WithMessage("Price must be non-negative.");

			RuleFor(x => x.ExpirationDate)
				.Must(date => date >= DateTime.Now.Date)
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
