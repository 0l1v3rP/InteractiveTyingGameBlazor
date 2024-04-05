using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveTyingGameBlazor.Migrations
{
    /// <inheritdoc />
    public partial class VideoMetada : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorrectChars",
                table: "RegisteredVideos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PressedChars",
                table: "RegisteredVideos",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TypingResults_VideoId",
                table: "TypingResults",
                column: "VideoId");

            migrationBuilder.AddForeignKey(
                name: "FK_TypingResults_RegisteredVideos_VideoId",
                table: "TypingResults",
                column: "VideoId",
                principalTable: "RegisteredVideos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TypingResults_RegisteredVideos_VideoId",
                table: "TypingResults");

            migrationBuilder.DropIndex(
                name: "IX_TypingResults_VideoId",
                table: "TypingResults");

            migrationBuilder.DropColumn(
                name: "CorrectChars",
                table: "RegisteredVideos");

            migrationBuilder.DropColumn(
                name: "PressedChars",
                table: "RegisteredVideos");
        }
    }
}
