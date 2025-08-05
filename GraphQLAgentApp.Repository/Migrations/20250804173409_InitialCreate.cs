using Microsoft.EntityFrameworkCore.Migrations;
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GraphQLAgentApp.Repository.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CreatedAt", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "Andrew Hunt", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Pragmatic Programmer", null },
                    { 2, "Robert C. Martin", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Clean Code", null },
                    { 3, "Eric Evans", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Domain-Driven Design", null },
                    { 4, "Martin Fowler", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Refactoring", null },
                    { 5, "Kent Beck", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test-Driven Development", null },
                    { 6, "Martin Fowler", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Patterns of Enterprise Application Architecture", null },
                    { 7, "Michael Feathers", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Working Effectively with Legacy Code", null },
                    { 8, "Robert C. Martin", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "The Clean Coder", null },
                    { 10, "Kyle Simpson", new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "You Don't Know JS", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
