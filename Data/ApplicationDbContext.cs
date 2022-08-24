using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Demo.Models;

namespace MVC_Demo.Data
{
    public partial class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseMySql("server=localhost;port=3306;user=root;database=mvc_demo_auth", new MySqlServerVersion(new Version(10, 4, 24)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.Property(e => e.Description)
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasData(
                    new ProductCategory[]
                    {
                        new ProductCategory() {ProductCategoryID = 1, Name = "Dairy", Description = "Stuff that indirectly comes from cows."},
                        new ProductCategory() {ProductCategoryID = 2, Name = "Deli", Description = "Stuff that comes from cows, pigs, chickens, etc."},
                        new ProductCategory() {ProductCategoryID = 3, Name = "Garden", Description = "Fruits and vegitables."},
                        new ProductCategory() {ProductCategoryID = 4, Name = "Beverages", Description = "Stuff that you drink."},
                        new ProductCategory() {ProductCategoryID = 5, Name = "Frozen", Description = "Stuff that's stored below freezing."},
                        new ProductCategory() {ProductCategoryID = 6, Name = "Cereal", Description = "Stuff that's either healthy or tastes good."}
                    });
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasIndex(e => e.CategoryID)
                    .HasName("FK_" + nameof(Product) + "_" + nameof(ProductCategory));

                entity.Property(e => e.Name)
                    .HasCharSet("utf8mb4")
                    .UseCollation("utf8mb4_general_ci");

                entity.HasOne(x => x.ProductCategory)
                    .WithMany(y => y.Products)
                    .HasForeignKey(x => x.CategoryID)
                    .HasConstraintName("FK_" + nameof(Product) + "_" + nameof(ProductCategory))
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasData(
                    new Product[]
                    {
                        new Product() {ProductID = 1, CategoryID = 1, Name="Milk", QuantityOnHand = 10, SalePrice = 2.50m},
                        new Product()  {ProductID = 2, CategoryID = 6, Name="Cereal", QuantityOnHand = 50, SalePrice = 1.25m},
                        new Product()  {ProductID = 3, CategoryID = 3, Name="Broccoli", QuantityOnHand = 20, SalePrice = 1.50m}
                    });
            });



            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}