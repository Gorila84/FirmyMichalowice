using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FirmyMichalowice.Migrations
{
    public partial class CompanyTypeInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         

            //migrationBuilder.CreateTable(
            //    name: "CompanyTypes",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
            //        Name = table.Column<string>(type: "longtext", nullable: true)
            //            .Annotation("MySql:CharSet", "utf8mb4"),
            //        Discription = table.Column<string>(type: "longtext", nullable: true)
            //            .Annotation("MySql:CharSet", "utf8mb4")
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_CompanyTypes", x => x.Id);
            //    })
            //    .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
