using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirmyMichalowice.Migrations
{
    public partial class addDateToCompanySettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LinkVisibilityEnd",
                table: "CompanySettings",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LinkVisibilityStart",
                table: "CompanySettings",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OfferVisibilityEnd",
                table: "CompanySettings",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "OfferVisibilityStart",
                table: "CompanySettings",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PKDVisibilityEnd",
                table: "CompanySettings",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PKDVisibilityStart",
                table: "CompanySettings",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkVisibilityEnd",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "LinkVisibilityStart",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "OfferVisibilityEnd",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "OfferVisibilityStart",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "PKDVisibilityEnd",
                table: "CompanySettings");

            migrationBuilder.DropColumn(
                name: "PKDVisibilityStart",
                table: "CompanySettings");
        }
    }
}
