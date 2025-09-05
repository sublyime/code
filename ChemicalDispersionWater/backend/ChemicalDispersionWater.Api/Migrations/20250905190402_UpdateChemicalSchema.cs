using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ChemicalDispersionWater.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateChemicalSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "BoilingPointC",
                table: "Chemicals",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Density",
                table: "Chemicals",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "MolecularWeight",
                table: "Chemicals",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "SolubilityMgL",
                table: "Chemicals",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.InsertData(
                table: "Chemicals",
                columns: new[] { "Id", "BoilingPointC", "Density", "MolecularWeight", "Name", "SolubilityMgL" },
                values: new object[,]
                {
                    { 1, 0.0, 0.84999999999999998, 0.0, "Oil", 0.0 },
                    { 2, 0.0, 1.2, 0.0, "Acid", 0.0 }
                });

            migrationBuilder.InsertData(
                table: "Spills",
                columns: new[] { "Id", "ChemicalId", "Location", "Timestamp", "Volume" },
                values: new object[,]
                {
                    { 1, 1, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-122.4194 37.7749)"), new DateTime(2025, 9, 5, 19, 4, 2, 337, DateTimeKind.Utc).AddTicks(9238), 1000.0 },
                    { 2, 2, (NetTopologySuite.Geometries.Point)new NetTopologySuite.IO.WKTReader().Read("SRID=4326;POINT (-122.5 37.8)"), new DateTime(2025, 9, 5, 18, 4, 2, 338, DateTimeKind.Utc).AddTicks(2031), 500.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Spills",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Spills",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Chemicals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Chemicals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "BoilingPointC",
                table: "Chemicals");

            migrationBuilder.DropColumn(
                name: "Density",
                table: "Chemicals");

            migrationBuilder.DropColumn(
                name: "MolecularWeight",
                table: "Chemicals");

            migrationBuilder.DropColumn(
                name: "SolubilityMgL",
                table: "Chemicals");
        }
    }
}
