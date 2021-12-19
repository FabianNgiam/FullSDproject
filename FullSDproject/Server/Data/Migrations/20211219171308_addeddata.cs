using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FullSDproject.Server.Data.Migrations
{
    public partial class addeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2021, 12, 20, 1, 13, 8, 8, DateTimeKind.Local).AddTicks(138), new DateTime(2021, 12, 20, 1, 13, 8, 8, DateTimeKind.Local).AddTicks(7602) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2021, 12, 20, 1, 13, 8, 8, DateTimeKind.Local).AddTicks(8449), new DateTime(2021, 12, 20, 1, 13, 8, 8, DateTimeKind.Local).AddTicks(8453) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2021, 12, 20, 1, 4, 52, 285, DateTimeKind.Local).AddTicks(2245), new DateTime(2021, 12, 20, 1, 4, 52, 286, DateTimeKind.Local).AddTicks(615) });

            migrationBuilder.UpdateData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DateCreated", "DateUpdated" },
                values: new object[] { new DateTime(2021, 12, 20, 1, 4, 52, 286, DateTimeKind.Local).AddTicks(1490), new DateTime(2021, 12, 20, 1, 4, 52, 286, DateTimeKind.Local).AddTicks(1495) });
        }
    }
}
