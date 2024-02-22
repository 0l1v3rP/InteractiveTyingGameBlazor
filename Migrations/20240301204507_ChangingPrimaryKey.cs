using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InteractiveTyingGameBlazor.Migrations
{
    /// <inheritdoc />
    public partial class ChangingPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypingResultId",
                table: "TypingResults",
                newName: "Id");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "MissTypedChar",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "MissTypedChar");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TypingResults",
                newName: "TypingResultId");
        }
    }
}
