using MediatR;

namespace Clean.Architecture.Inventory.Application.Commands
{
	public class CreateProductCommand : IRequest<int>
	{
		public string Name { get; set; }
		public string Descritpion { get; set; }
		public double Price { get; set; }
		public string Photo { get; set; }

		public DateTime ExpirationDate { get; set; }
		public int CategoryId { get; set; }
	}
}
