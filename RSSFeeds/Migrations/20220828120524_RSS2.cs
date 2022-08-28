using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RSSFeeds.Migrations
{
    public partial class RSS2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isReaded",
                table: "RSS",
                newName: "isRead");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isRead",
                table: "RSS",
                newName: "isReaded");
        }
    }
}
