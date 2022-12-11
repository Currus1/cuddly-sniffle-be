using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace currus.Migrations
{
    public partial class tripdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TripDate",
                table: "Trip",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TripDate",
                table: "Trip");
        }
    }
}
