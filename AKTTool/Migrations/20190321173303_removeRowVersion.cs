using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AKTTool.Migrations
{
    public partial class removeRowVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "generals");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "generals",
                nullable: true);
        }
    }
}
