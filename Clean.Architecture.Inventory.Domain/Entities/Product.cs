namespace Clean.Architecture.Inventory.Domain.Entities
{
	public class Product
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public DateTime CreatedAt { get; set; }
		public DateTime UpdatedAt { get; set; }
		public DateTime ExpirationDate { get; set; }
		public string Photo { get; set; }
		// Relação com Categoria
		public int CategoryId { get; set; }
		public CategoryProduct CategoryProducts { get; set; }
	}
}
