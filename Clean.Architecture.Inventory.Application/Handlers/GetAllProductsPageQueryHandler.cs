using Clean.Architecture.Inventory.Application.Interfaces;
using Clean.Architecture.Inventory.Application.Queries;
using Clean.Architecture.Inventory.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Clean.Architecture.Inventory.Application.Handlers
{
	public class GetAllProductsPageQueryHandler : IRequestHandler<GetAllProductPageQuery, PagedResult<Product>>
	{
		private readonly IProductRepository _context;

		public GetAllProductsPageQueryHandler(IProductRepository context)
		{
			_context = context;
		}

		public async Task<PagedResult<Product>> Handle(GetAllProductPageQuery request, CancellationToken cancellationToken)
		{
			// Obter IQueryable para poder aplicar paginação
			var query = await _context.GetAllQueryableAsync();

			// Contar o total de produtos
			var totalProducts = await query.CountAsync(cancellationToken);

			// Aplicar paginação
			var products = await query
				.Skip((request.PageNumber - 1) * request.PageSize) // Pula os produtos das páginas anteriores
				.Take(request.PageSize) // Pega a quantidade definida na página
				.ToListAsync(cancellationToken); // Executa a consulta de forma assíncrona

			return new PagedResult<Product>
			{
				Items = products,
				TotalCount = totalProducts
			};
		}
	}
}
