using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Badge.Migrations
{
    public partial class AddThresholdToBadge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AwardThreshold",
                table: "Badges",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentThreshold",
                table: "Badges",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AwardThreshold",
                table: "Badges");

            migrationBuilder.DropColumn(
                name: "CurrentThreshold",
                table: "Badges");
        }
    }
}
