using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EchoPost.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddChannelToPost : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Channel",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Channel",
                table: "Posts");
        }
    }
}
