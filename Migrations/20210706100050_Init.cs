using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace iaccess_test.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "T_SearchString",
                columns: table => new
                {
                    String_ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    String_Content = table.Column<string>(type: "nvarchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_SearchString", x => x.String_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "T_SearchString");
        }
    }
}
