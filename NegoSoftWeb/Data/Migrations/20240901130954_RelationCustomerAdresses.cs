using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegoSoftWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelationCustomerAdresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Customers_CustomerCusId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Customers_cus_default_address_id",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_CustomerCusId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "CustomerCusId",
                table: "Addresses");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_cus_default_address_id",
                table: "Customers",
                column: "cus_default_address_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_cus_default_address_id",
                table: "Customers");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerCusId",
                table: "Addresses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Customers_cus_default_address_id",
                table: "Customers",
                column: "cus_default_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerCusId",
                table: "Addresses",
                column: "CustomerCusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Customers_CustomerCusId",
                table: "Addresses",
                column: "CustomerCusId",
                principalTable: "Customers",
                principalColumn: "cus_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
