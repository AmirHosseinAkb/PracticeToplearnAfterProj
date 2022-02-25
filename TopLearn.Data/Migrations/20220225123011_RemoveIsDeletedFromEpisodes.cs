using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TopLearn.Data.Migrations
{
    public partial class RemoveIsDeletedFromEpisodes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CourseEpisodes");

            migrationBuilder.AlterColumn<string>(
                name: "EpisodeFileName",
                table: "CourseEpisodes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EpisodeFileName",
                table: "CourseEpisodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CourseEpisodes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
