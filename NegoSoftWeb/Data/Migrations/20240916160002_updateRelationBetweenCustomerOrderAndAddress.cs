using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegoSoftWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class updateRelationBetweenCustomerOrderAndAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerOrders_co_address_id",
                table: "CustomerOrders");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_co_address_id",
                table: "CustomerOrders",
                column: "co_address_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerOrders_co_address_id",
                table: "CustomerOrders");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_co_address_id",
                table: "CustomerOrders",
                column: "co_address_id",
                unique: true);
        }
    }
}
