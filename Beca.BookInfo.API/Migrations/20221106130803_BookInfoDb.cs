using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beca.BookInfo.API.Migrations
{
    public partial class BookInfoDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Books",
                type: "TEXT",
                maxLength: 300,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Cuento infantil.");

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Probablemente una de las mejores obras de Stefan Zwaig");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Books");
        }
    }
}
