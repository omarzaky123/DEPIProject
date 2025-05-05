using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntialRepo.EF.Migrations
{
    /// <inheritdoc />
    public partial class ProductVartionProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductID",
                table: "ProductVartions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ProductVartions_ProductID",
                table: "ProductVartions",
                column: "ProductID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVartions_Products_ProductID",
                table: "ProductVartions",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVartions_Products_ProductID",
                table: "ProductVartions");

            migrationBuilder.DropIndex(
                name: "IX_ProductVartions_ProductID",
                table: "ProductVartions");

            migrationBuilder.DropColumn(
                name: "ProductID",
                table: "ProductVartions");
        }
    }
}
