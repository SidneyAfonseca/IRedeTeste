using Clean.Architecture.Inventory.Domain.Entities;
using MediatR;

namespace Clean.Architecture.Inventory.Application.Queries
{
	public class GetAllProductPageQuery : IRequest<PagedResult<Product>>
	{
		public int PageNumber { get; set; } = 1; // Página inicial
		public int PageSize { get; set; } = 10; // Tamanho da página


		public GetAllProductPageQuery(int pageNumber, int pageSize)
		{
			PageNumber = pageNumber;
			PageSize = pageSize;
		}
	}
}
