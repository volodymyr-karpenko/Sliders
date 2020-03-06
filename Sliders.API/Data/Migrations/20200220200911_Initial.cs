using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Sliders.API.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SlidersData",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Time = table.Column<DateTime>(nullable: false),
                    Slider1 = table.Column<int>(nullable: false),
                    Slider2 = table.Column<int>(nullable: false),
                    Slider3 = table.Column<int>(nullable: false),
                    Slider4 = table.Column<int>(nullable: false),
                    Slider5 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlidersData", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SlidersData");
        }
    }
}