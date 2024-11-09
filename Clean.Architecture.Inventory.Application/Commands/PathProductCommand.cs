using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Inventory.Application.Commands
{
	public class PathProductCommand : IRequest<Unit>
	{
		public int Id { get; set; }

		public double Price { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

	}
}
