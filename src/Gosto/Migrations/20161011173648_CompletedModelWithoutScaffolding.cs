using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Gosto.Models;

namespace Gosto.Migrations
{
    public partial class CompletedModelWithoutScaffolding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTakeAway",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "TakeAway",
                table: "Order",
                nullable: false,
                defaultValue: TakeAway.No);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TakeAway",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "IsTakeAway",
                table: "Order",
                nullable: false,
                defaultValue: 0);
        }
    }
}
