using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegoSoftWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "cus_user_id",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_cus_user_id",
                table: "Customers",
                column: "cus_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_AspNetUsers_cus_user_id",
                table: "Customers",
                column: "cus_user_id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_AspNetUsers_cus_user_id",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_cus_user_id",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "cus_user_id",
                table: "Customers");
        }
    }
}
