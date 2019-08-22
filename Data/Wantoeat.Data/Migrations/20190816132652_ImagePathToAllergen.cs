using Microsoft.EntityFrameworkCore.Migrations;

namespace Wantoeat.Data.Migrations
{
    public partial class ImagePathToAllergen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Allergens",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }
    }
}
