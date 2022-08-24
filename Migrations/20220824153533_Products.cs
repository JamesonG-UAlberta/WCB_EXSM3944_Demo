using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVC_Demo.Migrations
{
    public partial class Products : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "product_category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_category", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int(10)", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    category_id = table.Column<int>(type: "int(10)", nullable: false),
                    name = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false, collation: "utf8mb4_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    qoh = table.Column<int>(type: "int(10)", nullable: false),
                    reorderthreshold = table.Column<int>(type: "int(10)", nullable: true),
                    saleprice = table.Column<decimal>(type: "decimal(5,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_Product_ProductCategory",
                        column: x => x.category_id,
                        principalTable: "product_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "product_category",
                columns: new[] { "id", "description", "name" },
                values: new object[,]
                {
                    { 1, "Stuff that indirectly comes from cows.", "Dairy" },
                    { 2, "Stuff that comes from cows, pigs, chickens, etc.", "Deli" },
                    { 3, "Fruits and vegitables.", "Garden" },
                    { 4, "Stuff that you drink.", "Beverages" },
                    { 5, "Stuff that's stored below freezing.", "Frozen" },
                    { 6, "Stuff that's either healthy or tastes good.", "Cereal" }
                });

            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "id", "category_id", "name", "qoh", "reorderthreshold", "saleprice" },
                values: new object[] { 1, 1, "Milk", 10, null, 2.50m });

            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "id", "category_id", "name", "qoh", "reorderthreshold", "saleprice" },
                values: new object[] { 2, 6, "Cereal", 50, null, 1.25m });

            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "id", "category_id", "name", "qoh", "reorderthreshold", "saleprice" },
                values: new object[] { 3, 3, "Broccoli", 20, null, 1.50m });

            migrationBuilder.CreateIndex(
                name: "FK_Product_ProductCategory",
                table: "product",
                column: "category_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "product_category");
        }
    }
}
