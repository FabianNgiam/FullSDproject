using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FullSDproject.Server.Data.Migrations
{
    public partial class AddApplicationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Requirements = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rating = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Developer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Publisher = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateUpdated", "Developer", "Genre", "Price", "Publisher", "Rating", "ReleaseDate", "Requirements", "Title", "UpdatedBy" },
                values: new object[] { 1, "System", new DateTime(2021, 12, 20, 1, 4, 52, 285, DateTimeKind.Local).AddTicks(2245), new DateTime(2021, 12, 20, 1, 4, 52, 286, DateTimeKind.Local).AddTicks(615), "Cheesy Studios", "Puzzle", 2f, "Milk Games", "PG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intel Core i5 or higher, NVidia GTX 1650, 8GB of RAM, 10GB of free disk space", "Cheese The Game", "System" });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "CreatedBy", "DateCreated", "DateUpdated", "Developer", "Genre", "Price", "Publisher", "Rating", "ReleaseDate", "Requirements", "Title", "UpdatedBy" },
                values: new object[] { 2, "System", new DateTime(2021, 12, 20, 1, 4, 52, 286, DateTimeKind.Local).AddTicks(1490), new DateTime(2021, 12, 20, 1, 4, 52, 286, DateTimeKind.Local).AddTicks(1495), "Cheesy Studios", "Puzzle", 3f, "Milk Games", "PG", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Intel Core i7 or higher, NVidia RTX 2060, 8GB of RAM, 10GB of free disk space", "Cheese The Game 2", "System" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
