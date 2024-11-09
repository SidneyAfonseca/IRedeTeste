using Clean.Architecture.Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Clean.Architecture.Inventory.Persistence
{
	public class InventoryControlDbContext : DbContext
	{
		public InventoryControlDbContext(DbContextOptions<InventoryControlDbContext> options)
			: base(options)
		{
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<CategoryProduct> CategoryProducts { get; set; }
		public DbSet<ErrorLog> ErrorLogs { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{

			modelBuilder.Entity<Product>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
				entity.Property(e => e.Description).IsRequired().HasMaxLength(200);
				entity.Property(p => p.ExpirationDate)
				  .IsRequired();
				entity.Property(p => p.Photo)
					  .HasMaxLength(2048);
				entity.Property(p => p.Price).IsRequired();
				entity.HasOne(p => p.CategoryProducts).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId).OnDelete(DeleteBehavior.Cascade);
			});

			modelBuilder.Entity<CategoryProduct>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
				entity.HasIndex(e => e.Name).IsUnique().HasDatabaseName("IX_CategoryProduct_Name");
				// Seed para CategoryProduct
				entity.HasData(
					new CategoryProduct { Id = 1, Name = "Electronics" },
					new CategoryProduct { Id = 2, Name = "Clothing" },
					new CategoryProduct { Id = 3, Name = "Furniture" },
					new CategoryProduct { Id = 4, Name = "Toys" },
					new CategoryProduct { Id = 5, Name = "Books" }
				);
			});

			modelBuilder.Entity<ErrorLog>(entity =>
			{
				entity.HasKey(e => e.Id);
				entity.Property(e => e.Message).IsRequired();
				entity.Property(e => e.StackTrace).IsRequired(false);
				entity.Property(e => e.OccurredAt).IsRequired();
			});

			base.OnModelCreating(modelBuilder);
		}
	}
}
