using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace project5_6.Migrations
{
    public partial class viewcount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "viewcount",
                table: "Laptop",
                type: "int4",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "viewcount",
                table: "Laptop");
        }
    }
}
