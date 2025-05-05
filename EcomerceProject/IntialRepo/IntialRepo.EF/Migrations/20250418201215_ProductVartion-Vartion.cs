using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntialRepo.EF.Migrations
{
    /// <inheritdoc />
    public partial class ProductVartionVartion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VartionID",
                table: "ProductVartions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductVartions_VartionID",
                table: "ProductVartions",
                column: "VartionID");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductVartions_Vartions_VartionID",
                table: "ProductVartions",
                column: "VartionID",
                principalTable: "Vartions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductVartions_Vartions_VartionID",
                table: "ProductVartions");

            migrationBuilder.DropIndex(
                name: "IX_ProductVartions_VartionID",
                table: "ProductVartions");

            migrationBuilder.DropColumn(
                name: "VartionID",
                table: "ProductVartions");
        }
    }
}
