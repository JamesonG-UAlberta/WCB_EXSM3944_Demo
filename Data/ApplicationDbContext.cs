using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Demo.Models;

namespace MVC_Demo.Data
{
    /*
     DbContext:
        -Copy (or write) the contents of the DbContext from a previous completed one (I used the ef-iii demo from EXSM3942).
        -Set the class to partial.
        -Change the database name in OnConfiguring (it's a fallback from appsettings.json so it shouldn't matter, but set it anyways).
        -Add a call to the base version of OnModelCreating().
        -(If you copied from the same one I did) Change the format for the Product seed data to a full initializer list on a default constructor (similar to the ProductCategory seed data).
     Models:
        -Copy (or write) the model files.
        -(If you copied from the same one I did) remove all non-default constructors, NotMapped properties (these will return later), and logic methods (CRUD).
        -Add a migration and update the database.
     Project Config (csproj):
        -Comment out the line at the beginning: <Nullable>enable</Nullable>
     Controller/View Creation:
        -Right click on Controllers, add your controllers.
        -Add links to their Index action to the navigation.
    */
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