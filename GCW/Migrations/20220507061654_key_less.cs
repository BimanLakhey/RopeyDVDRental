using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GCW.Migrations
{
    public partial class key_less : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CastMember",
                columns: table => new
                {
                    DVDNumber = table.Column<int>(type: "int", nullable: false),
                    ActorNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_CastMember_Actor_ActorNumber",
                        column: x => x.ActorNumber,
                        principalTable: "Actor",
                        principalColumn: "ActorNumber",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CastMember_DVDTitle_DVDNumber",
                        column: x => x.DVDNumber,
                        principalTable: "DVDTitle",
                        principalColumn: "DVDNumber",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CastMember_ActorNumber",
                table: "CastMember",
                column: "ActorNumber");

            migrationBuilder.CreateIndex(
                name: "IX_CastMember_DVDNumber",
                table: "CastMember",
                column: "DVDNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CastMember");
        }
    }
}
