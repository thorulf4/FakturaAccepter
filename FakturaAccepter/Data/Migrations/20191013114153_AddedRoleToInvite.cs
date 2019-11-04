using Microsoft.EntityFrameworkCore.Migrations;

namespace FakturaAccepter.Data.Migrations
{
    public partial class AddedRoleToInvite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "InviteLinks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Role",
                table: "InviteLinks");
        }
    }
}
