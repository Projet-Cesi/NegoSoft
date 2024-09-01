using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegoSoftWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class RelationOrdersAdresses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SupplierOrders_so_address_id",
                table: "SupplierOrders");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrders_co_address_id",
                table: "CustomerOrders");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrders_so_address_id",
                table: "SupplierOrders",
                column: "so_address_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_co_address_id",
                table: "CustomerOrders",
                column: "co_address_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SupplierOrders_so_address_id",
                table: "SupplierOrders");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrders_co_address_id",
                table: "CustomerOrders");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrders_so_address_id",
                table: "SupplierOrders",
                column: "so_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_co_address_id",
                table: "CustomerOrders",
                column: "co_address_id");
        }
    }
}
