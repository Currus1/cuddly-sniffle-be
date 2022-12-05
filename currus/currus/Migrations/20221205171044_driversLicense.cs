using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace currus.Migrations
{
    public partial class driversLicense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DriversLicense",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriversLicense",
                table: "AspNetUsers");
        }
    }
}
