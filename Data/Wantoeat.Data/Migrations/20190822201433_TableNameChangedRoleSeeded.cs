using Microsoft.EntityFrameworkCore.Migrations;

namespace Wantoeat.Data.Migrations
{
    public partial class TableNameChangedRoleSeeded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(name: "Comment", schema: "dbo", newName: "Comments", newSchema: "dbo");

            /*migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "Name", "CreatedOn", "IsDeleted" },
                values: new object[] { "2", "User", "2019-08-22 23:28", false });*/
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(name: "Comments", schema: "dbo", newName: "Comment", newSchema: "dbo");
        }
    }
}
