using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApp.Migrations
{
	/// <inheritdoc />  
	public partial class Initial : Migration
	{
		/// <inheritdoc />  
		protected override void Up(MigrationBuilder migrationBuilder)
		{
			if (migrationBuilder == null)
			{
				throw new ArgumentNullException(nameof(migrationBuilder));
			}

			_ = migrationBuilder.CreateTable(
				name: "Categories",
				columns: table => new
				{
					CategoryId = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table => _ = table.PrimaryKey("PK_Categories", x => x.CategoryId));

			_ = migrationBuilder.CreateTable(
				name: "Suppliers",
				columns: table => new
				{
					SupplierId = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					City = table.Column<string>(type: "nvarchar(max)", nullable: false)
				},
				constraints: table => table.PrimaryKey("PK_Suppliers", x => x.SupplierId));

			_ = migrationBuilder.CreateTable(
				name: "Products",
				columns: table => new
				{
					ProductId = table.Column<long>(type: "bigint", nullable: false)
						.Annotation("SqlServer:Identity", "1, 1"),
					Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
					Price = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
					CategoryId = table.Column<long>(type: "bigint", nullable: false),
					SupplierId = table.Column<long>(type: "bigint", nullable: false)
				},
				constraints: table =>
				{
					_ = table.PrimaryKey("PK_Products", x => x.ProductId);
					_ = table.ForeignKey(
						name: "FK_Products_Categories_CategoryId",
						column: x => x.CategoryId,
						principalTable: "Categories",
						principalColumn: "CategoryId",
						onDelete: ReferentialAction.Cascade);
					_ = table.ForeignKey(
						name: "FK_Products_Suppliers_SupplierId",
						column: x => x.SupplierId,
						principalTable: "Suppliers",
						principalColumn: "SupplierId",
						onDelete: ReferentialAction.Cascade);
				});

			_ = migrationBuilder.CreateIndex(
				name: "IX_Products_CategoryId",
				table: "Products",
				column: "CategoryId");

			_ = migrationBuilder.CreateIndex(
				name: "IX_Products_SupplierId",
				table: "Products",
				column: "SupplierId");
		}

		/// <inheritdoc />  
		protected override void Down(MigrationBuilder migrationBuilder)
		{
			if (migrationBuilder == null)
			{
				throw new ArgumentNullException(nameof(migrationBuilder));
			}

			_ = migrationBuilder.DropTable(
				name: "Products");

			_ = migrationBuilder.DropTable(
				name: "Categories");

			_ = migrationBuilder.DropTable(
				name: "Suppliers");
		}
	}
}
