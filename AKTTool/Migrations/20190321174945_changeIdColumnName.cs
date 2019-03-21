using Microsoft.EntityFrameworkCore.Migrations;

namespace AKTTool.Migrations
{
    public partial class changeIdColumnName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "generals",
                newName: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "generals",
                newName: "Id");
        }
    }
}
