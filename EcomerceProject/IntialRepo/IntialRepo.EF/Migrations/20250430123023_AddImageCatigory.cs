using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IntialRepo.EF.Migrations
{
    /// <inheritdoc />
    public partial class AddImageCatigory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Catigorys",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Catigorys");
        }
    }
}
