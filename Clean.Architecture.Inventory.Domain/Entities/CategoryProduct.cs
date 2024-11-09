using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Inventory.Domain.Entities
{

	public class CategoryProduct
	{
		public int Id { get; set; }

		public string Name { get; set; }

		public ICollection<Product> Products { get; set; }
	}
}
