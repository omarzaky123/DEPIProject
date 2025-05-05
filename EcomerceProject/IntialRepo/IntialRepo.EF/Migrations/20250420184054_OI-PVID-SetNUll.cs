using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntialRepo.EF.Migrations
{
    /// <inheritdoc />
    public partial class OIPVIDSetNUll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductVartions_ProductVartionId",
                table: "OrderItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProductVartionId",
                table: "OrderItems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductVartions_ProductVartionId",
                table: "OrderItems",
                column: "ProductVartionId",
                principalTable: "ProductVartions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_ProductVartions_ProductVartionId",
                table: "OrderItems");

            migrationBuilder.AlterColumn<int>(
                name: "ProductVartionId",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_ProductVartions_ProductVartionId",
                table: "OrderItems",
                column: "ProductVartionId",
                principalTable: "ProductVartions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
