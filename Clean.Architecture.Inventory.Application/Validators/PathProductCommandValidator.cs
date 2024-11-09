using Clean.Architecture.Inventory.Application.Commands;
using FluentValidation;

namespace Clean.Architecture.Inventory.Application.Validators
{
	public class PathProductCommandValidator : AbstractValidator<PathProductCommand>
	{
		public PathProductCommandValidator()
		{
			RuleFor(x => x.Id)
				.GreaterThan(0).WithMessage("Invalid product ID.");


			RuleFor(x => x.Price)
				.GreaterThanOrEqualTo(0).WithMessage("Average price must be non-negative.");

		}
	}
}
