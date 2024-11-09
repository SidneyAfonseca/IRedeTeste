using Clean.Architecture.Inventory.Application.Commands;
using Clean.Architecture.Inventory.Application.Queries;
using Clean.Architecture.Inventory.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Clean.Architecture.Inventory.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ProductsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		// GET: api/Products
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetAll()
		{
			var query = new GetAllProductsQuery();
			var products = await _mediator.Send(query);
			return Ok(products);
		}

		// GET: api/Products/GetPage
		[HttpGet]
		[Route("GetPage")]
		public async Task<ActionResult<PagedResult<Product>>> GetPage([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
		{
			var query = new GetAllProductPageQuery(pageNumber, pageSize);
			var products = await _mediator.Send(query);
			return Ok(products);
		}

		// GET: api/Products/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetById(int id)
		{
			var query = new GetProductByIdQuery { Id = id };
			var product = await _mediator.Send(query);
			if (product == null)
				return NotFound();
			return Ok(product);
		}

		// GET: api/Products/{text}
		[HttpGet]
		[Route("text")]
		public async Task<ActionResult<Product>> GetBytext(string text)
		{
			var query = new GetProductByTextQuery { Text = text };
			var product = await _mediator.Send(query);
			if (product == null)
				return NotFound();
			return Ok(product);
		}

		// POST: api/Products
		[HttpPost]
		public async Task<ActionResult<int>> Create([FromBody] CreateProductCommand command)
		{
			var productId = await _mediator.Send(command);
			return CreatedAtAction(nameof(GetById), new { id = productId }, productId);
		}

		// PUT: api/Products
		[HttpPut]
		public async Task<IActionResult> Update( [FromBody] UpdateProductCommand command)
		{
			if ( command.Id==0)
				return BadRequest("ID mismatch.");

			await _mediator.Send(command);
			return NoContent();
		}


		// PATH: api/Products/
		[HttpPatch]
		public async Task<IActionResult> Path( [FromBody] PathProductCommand command)
		{
			if ( command.Id ==0)
				return BadRequest("ID mismatch.");

			await _mediator.Send(command);
			return NoContent();
		}


		// DELETE: api/Products/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var command = new DeleteProductCommand { Id = id };
			await _mediator.Send(command);
			return NoContent();
		}
	}
}
