using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Steam.Migrations
{
    /// <inheritdoc />
    public partial class updatetblNews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfRelease",
                table: "tblNews",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Photo",
                table: "AspNetUsers",
                type: "character varying(2500)",
                maxLength: 2500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(2500)",
                oldMaxLength: 2500);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Photo",
                table: "AspNetUsers",
                type: "character varying(2500)",
                maxLength: 2500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(2500)",
                oldMaxLength: 2500,
                oldNullable: true);
        }
    }
}
