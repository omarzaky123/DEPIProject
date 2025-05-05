using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntialRepo.EF.Migrations
{
    /// <inheritdoc />
    public partial class CTPVSetNUll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ProductVartions_ProductVartionID",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProductVartionID",
                table: "CartItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ProductVartions_ProductVartionID",
                table: "CartItems",
                column: "ProductVartionID",
                principalTable: "ProductVartions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ProductVartions_ProductVartionID",
                table: "CartItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProductVartionID",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ProductVartions_ProductVartionID",
                table: "CartItems",
                column: "ProductVartionID",
                principalTable: "ProductVartions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
