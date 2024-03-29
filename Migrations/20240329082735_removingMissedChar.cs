using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveTyingGameBlazor.Migrations
{
    /// <inheritdoc />
    public partial class removingMissedChar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MissTypedChar");

            migrationBuilder.AddColumn<string>(
                name: "Chars",
                table: "TypingResults",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Chars",
                table: "TypingResults");

            migrationBuilder.CreateTable(
                name: "MissTypedChar",
                columns: table => new
                {
                    TypingResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MissTypedCharId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MissChar = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    Second = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissTypedChar", x => new { x.TypingResultId, x.MissTypedCharId });
                    table.ForeignKey(
                        name: "FK_MissTypedChar_TypingResults_TypingResultId",
                        column: x => x.TypingResultId,
                        principalTable: "TypingResults",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
