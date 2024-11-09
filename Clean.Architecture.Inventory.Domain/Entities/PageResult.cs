using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Architecture.Inventory.Domain.Entities
{
	public class PagedResult<T>
	{
		public IEnumerable<T> Items { get; set; }
		public int TotalCount { get; set; }
		public PagedResult()
		{
			Items = new List<T>();
		}
	}

}
