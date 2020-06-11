using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiCore.Migrations
{
    public partial class added_importedfileTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "AnonymousUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ImportFileDescriptionId",
                table: "AnonymousUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ImportFileDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CreatedTimestamp = table.Column<DateTime>(nullable: false),
                    ContentType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImportFileDescriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImportFileDescriptions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnonymousUsers_ImportFileDescriptionId",
                table: "AnonymousUsers",
                column: "ImportFileDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_ImportFileDescriptions_UserId",
                table: "ImportFileDescriptions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnonymousUsers_ImportFileDescriptions_ImportFileDescriptionId",
                table: "AnonymousUsers",
                column: "ImportFileDescriptionId",
                principalTable: "ImportFileDescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnonymousUsers_ImportFileDescriptions_ImportFileDescriptionId",
                table: "AnonymousUsers");

            migrationBuilder.DropTable(
                name: "ImportFileDescriptions");

            migrationBuilder.DropIndex(
                name: "IX_AnonymousUsers_ImportFileDescriptionId",
                table: "AnonymousUsers");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "AnonymousUsers");

            migrationBuilder.DropColumn(
                name: "ImportFileDescriptionId",
                table: "AnonymousUsers");
        }
    }
}
