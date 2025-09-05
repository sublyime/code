using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChemicalDispersionWater.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Spills",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2025, 9, 5, 19, 8, 12, 384, DateTimeKind.Utc).AddTicks(2213));

            migrationBuilder.UpdateData(
                table: "Spills",
                keyColumn: "Id",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2025, 9, 5, 18, 8, 12, 384, DateTimeKind.Utc).AddTicks(4794));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Spills",
                keyColumn: "Id",
                keyValue: 1,
                column: "Timestamp",
                value: new DateTime(2025, 9, 5, 19, 4, 2, 337, DateTimeKind.Utc).AddTicks(9238));

            migrationBuilder.UpdateData(
                table: "Spills",
                keyColumn: "Id",
                keyValue: 2,
                column: "Timestamp",
                value: new DateTime(2025, 9, 5, 18, 4, 2, 338, DateTimeKind.Utc).AddTicks(2031));
        }
    }
}
