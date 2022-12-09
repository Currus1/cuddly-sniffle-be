using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace currus.Migrations
{
    public partial class tripGeography : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Longitude",
                table: "Trip",
                newName: "SLongitude");

            migrationBuilder.RenameColumn(
                name: "Latitude",
                table: "Trip",
                newName: "SLatitude");

            migrationBuilder.AddColumn<double>(
                name: "DLatitude",
                table: "Trip",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "DLongitude",
                table: "Trip",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DLatitude",
                table: "Trip");

            migrationBuilder.DropColumn(
                name: "DLongitude",
                table: "Trip");

            migrationBuilder.RenameColumn(
                name: "SLongitude",
                table: "Trip",
                newName: "Longitude");

            migrationBuilder.RenameColumn(
                name: "SLatitude",
                table: "Trip",
                newName: "Latitude");
        }
    }
}
