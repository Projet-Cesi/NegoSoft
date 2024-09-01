using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegoSoftWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelationOrdersAdresses2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrders_Addresses_AddressAddId",
                table: "CustomerOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SupplierOrders_Addresses_AddressAddId",
                table: "SupplierOrders");

            migrationBuilder.DropIndex(
                name: "IX_SupplierOrders_AddressAddId",
                table: "SupplierOrders");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrders_AddressAddId",
                table: "CustomerOrders");

            migrationBuilder.DropColumn(
                name: "AddressAddId",
                table: "SupplierOrders");

            migrationBuilder.DropColumn(
                name: "AddressAddId",
                table: "CustomerOrders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AddressAddId",
                table: "SupplierOrders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AddressAddId",
                table: "CustomerOrders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrders_AddressAddId",
                table: "SupplierOrders",
                column: "AddressAddId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_AddressAddId",
                table: "CustomerOrders",
                column: "AddressAddId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrders_Addresses_AddressAddId",
                table: "CustomerOrders",
                column: "AddressAddId",
                principalTable: "Addresses",
                principalColumn: "add_id");

            migrationBuilder.AddForeignKey(
                name: "FK_SupplierOrders_Addresses_AddressAddId",
                table: "SupplierOrders",
                column: "AddressAddId",
                principalTable: "Addresses",
                principalColumn: "add_id");
        }
    }
}
