using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Beca.BookInfo.API.Migrations
{
    public partial class BookInfoDbContextInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Chapters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 300, nullable: true),
                    BookId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chapters_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Alicia en el país de las maravillas" });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Momentos estelares de la humanidad" });

            migrationBuilder.InsertData(
                table: "Chapters",
                columns: new[] { "Id", "BookId", "Description", "Name" },
                values: new object[] { 1, 1, "Érase una vez...", "El acceso a la madriguera" });

            migrationBuilder.InsertData(
                table: "Chapters",
                columns: new[] { "Id", "BookId", "Description", "Name" },
                values: new object[] { 2, 2, "En 1451...", "La conquista de Bizancio" });

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_BookId",
                table: "Chapters",
                column: "BookId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chapters");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
