using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace currus.Migrations
{
    public partial class RemoveFieldInTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InTrip",
                table: "User");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "InTrip",
                table: "User",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
