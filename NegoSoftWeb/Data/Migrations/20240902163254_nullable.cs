using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegoSoftWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_cus_default_address_id",
                table: "Customers");

            migrationBuilder.AlterColumn<Guid>(
                name: "cus_default_address_id",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_cus_default_address_id",
                table: "Customers",
                column: "cus_default_address_id",
                unique: true,
                filter: "[cus_default_address_id] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_cus_default_address_id",
                table: "Customers");

            migrationBuilder.AlterColumn<Guid>(
                name: "cus_default_address_id",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_cus_default_address_id",
                table: "Customers",
                column: "cus_default_address_id",
                unique: true);
        }
    }
}
