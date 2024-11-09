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
	public class CategotyProductController : ControllerBase
	{
		private readonly IMediator _mediator;

		public CategotyProductController(IMediator mediator)
		{
			_mediator = mediator;
		}

		// GET: api/CategotyProduct
		[HttpGet]
		public async Task<ActionResult<IEnumerable<CategoryProduct>>> GetAll()
		{
			var query = new GetAllCategoryProductsQuery();
			var CategotyProduct = await _mediator.Send(query);
			return Ok(CategotyProduct);
		}

		// GET: api/CategotyProduct/{id}
		[HttpGet("{id}")]
		public async Task<ActionResult<CategoryProduct>> GetById(int id)
		{
			var query = new GetCategoryProductByIdQuery { Id = id };
			var product = await _mediator.Send(query);
			if (product == null)
				return NotFound();
			return Ok(product);
		}

		//// POST: api/CategotyProduct
		//[HttpPost]
		//public async Task<ActionResult<int>> Create([FromBody] CreateCategoryProductCommand command)
		//{
		//	var productId = await _mediator.Send(command);
		//	return CreatedAtAction(nameof(GetById), new { id = productId }, productId);
		//}

		//// PUT: api/CategotyProduct/{id}
		//[HttpPut("{id}")]
		//public async Task<IActionResult> Update(int id, [FromBody] UpdateProductCommand command)
		//{
		//	if (id != command.Id)
		//		return BadRequest("ID mismatch.");

		//	await _mediator.Send(command);
		//	return NoContent();
		//}

		//// DELETE: api/CategotyProduct/{id}
		//[HttpDelete("{id}")]
		//public async Task<IActionResult> Delete(int id)
		//{
		//	var command = new DeleteProductCommand { Id = id };
		//	await _mediator.Send(command);
		//	return NoContent();
		//}
	}
}
