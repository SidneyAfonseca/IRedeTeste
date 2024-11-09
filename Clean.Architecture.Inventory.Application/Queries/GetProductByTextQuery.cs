using Clean.Architecture.Inventory.Domain.Entities;
using MediatR;

namespace Clean.Architecture.Inventory.Application.Queries
{
	public class GetProductByTextQuery : IRequest<IEnumerable<Product>>
	{
		public string Text { get; set; }
	}
}
