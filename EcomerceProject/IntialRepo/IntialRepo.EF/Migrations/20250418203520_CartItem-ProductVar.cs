using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntialRepo.EF.Migrations
{
    /// <inheritdoc />
    public partial class CartItemProductVar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductVartionID",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CartItems_ProductVartionID",
                table: "CartItems",
                column: "ProductVartionID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ProductVartions_ProductVartionID",
                table: "CartItems",
                column: "ProductVartionID",
                principalTable: "ProductVartions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ProductVartions_ProductVartionID",
                table: "CartItems");

            migrationBuilder.DropIndex(
                name: "IX_CartItems_ProductVartionID",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "ProductVartionID",
                table: "CartItems");
        }
    }
}
