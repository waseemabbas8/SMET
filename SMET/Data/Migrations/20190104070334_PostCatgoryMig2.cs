using Microsoft.EntityFrameworkCore.Migrations;

namespace SMET.Migrations
{
    public partial class PostCatgoryMig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "PostCategory",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "PostCategory",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
