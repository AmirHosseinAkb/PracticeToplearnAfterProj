using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopLearn.Data.Migrations
{
    public partial class AddIsDeletedColumnToRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Roles",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Roles");
        }
    }
}
