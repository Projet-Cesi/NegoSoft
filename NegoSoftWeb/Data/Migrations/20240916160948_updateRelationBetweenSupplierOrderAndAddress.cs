using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegoSoftWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateRelationBetweenSupplierOrderAndAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SupplierOrders_so_address_id",
                table: "SupplierOrders");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrders_so_address_id",
                table: "SupplierOrders",
                column: "so_address_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_SupplierOrders_so_address_id",
                table: "SupplierOrders");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrders_so_address_id",
                table: "SupplierOrders",
                column: "so_address_id",
                unique: true);
        }
    }
}
