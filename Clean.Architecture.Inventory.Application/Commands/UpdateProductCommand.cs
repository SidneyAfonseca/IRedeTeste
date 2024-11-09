using MediatR;

namespace Clean.Architecture.Inventory.Application.Commands
{
    public class UpdateProductCommand : IRequest<Unit>
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public DateTime ExpirationDate { get; set; }
		public string Photo { get; set; }
		public int CategoryId { get; set; }
	}
}
