using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveTyingGameBlazor.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MissTypedChar",
                columns: table => new
                {
                    MissTypedCharId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TypingResultId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Second = table.Column<int>(type: "int", nullable: false),
                    MissChar = table.Column<string>(type: "nvarchar(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissTypedChar", x => new { x.TypingResultId, x.MissTypedCharId });
                    table.ForeignKey(
                        name: "FK_MissTypedChar_TypingResults_TypingResultId",
                        column: x => x.TypingResultId,
                        principalTable: "TypingResults",
                        principalColumn: "TypingResultId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MissTypedChar");
        }
    }
}
