using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NegoSoftWeb.Data.Migrations
{
    /// <inheritdoc />
    public partial class InititalCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    typ_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    typ_libelle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.typ_id);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    add_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    add_delivery_street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    add_delivery_city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    add_delivery_zip_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    add_delivery_country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    add_billing_street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    add_billing_city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    add_billing_zip_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    add_billing_country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerCusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.add_id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    cus_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cus_first_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cus_last_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cus_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cus_phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    cus_default_address_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.cus_id);
                    table.ForeignKey(
                        name: "FK_Customers_Addresses_cus_default_address_id",
                        column: x => x.cus_default_address_id,
                        principalTable: "Addresses",
                        principalColumn: "add_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    sup_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sup_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sup_default_address_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sup_email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sup_phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.sup_id);
                    table.ForeignKey(
                        name: "FK_Suppliers_Addresses_sup_default_address_id",
                        column: x => x.sup_default_address_id,
                        principalTable: "Addresses",
                        principalColumn: "add_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrders",
                columns: table => new
                {
                    co_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    co_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    co_state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    co_customer_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    co_address_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    co_total = table.Column<float>(type: "real", nullable: false),
                    AddressAddId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrders", x => x.co_id);
                    table.ForeignKey(
                        name: "FK_CustomerOrders_Addresses_AddressAddId",
                        column: x => x.AddressAddId,
                        principalTable: "Addresses",
                        principalColumn: "add_id");
                    table.ForeignKey(
                        name: "FK_CustomerOrders_Addresses_co_address_id",
                        column: x => x.co_address_id,
                        principalTable: "Addresses",
                        principalColumn: "add_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerOrders_Customers_co_customer_id",
                        column: x => x.co_customer_id,
                        principalTable: "Customers",
                        principalColumn: "cus_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    pro_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    pro_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pro_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pro_supplier_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    pro_price = table.Column<float>(type: "real", nullable: false),
                    pro_type_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    pro_stock = table.Column<int>(type: "int", nullable: false),
                    pro_picture_name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.pro_id);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_pro_supplier_id",
                        column: x => x.pro_supplier_id,
                        principalTable: "Suppliers",
                        principalColumn: "sup_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Products_Types_pro_type_id",
                        column: x => x.pro_type_id,
                        principalTable: "Types",
                        principalColumn: "typ_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplierOrders",
                columns: table => new
                {
                    so_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    so_total = table.Column<float>(type: "real", nullable: false),
                    so_supplier_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    so_address_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    so_state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    so_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AddressAddId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierOrders", x => x.so_id);
                    table.ForeignKey(
                        name: "FK_SupplierOrders_Addresses_AddressAddId",
                        column: x => x.AddressAddId,
                        principalTable: "Addresses",
                        principalColumn: "add_id");
                    table.ForeignKey(
                        name: "FK_SupplierOrders_Addresses_so_address_id",
                        column: x => x.so_address_id,
                        principalTable: "Addresses",
                        principalColumn: "add_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierOrders_Suppliers_so_supplier_id",
                        column: x => x.so_supplier_id,
                        principalTable: "Suppliers",
                        principalColumn: "sup_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AlcoholProducts",
                columns: table => new
                {
                    ap_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    pro_type_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ap_alcohol_volume = table.Column<float>(type: "real", nullable: false),
                    ap_year = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlcoholProducts", x => x.ap_id);
                    table.ForeignKey(
                        name: "FK_AlcoholProducts_Products_pro_type_id",
                        column: x => x.pro_type_id,
                        principalTable: "Products",
                        principalColumn: "pro_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrderDetails",
                columns: table => new
                {
                    cod_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cod_quantity = table.Column<int>(type: "int", nullable: false),
                    cod_order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cod_product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    cod_price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrderDetails", x => x.cod_id);
                    table.ForeignKey(
                        name: "FK_CustomerOrderDetails_CustomerOrders_cod_order_id",
                        column: x => x.cod_order_id,
                        principalTable: "CustomerOrders",
                        principalColumn: "co_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CustomerOrderDetails_Products_cod_product_id",
                        column: x => x.cod_product_id,
                        principalTable: "Products",
                        principalColumn: "pro_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplierOrderDetails",
                columns: table => new
                {
                    sod_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sod_quantity = table.Column<int>(type: "int", nullable: false),
                    sod_order_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sod_product_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    sod_price = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierOrderDetails", x => x.sod_id);
                    table.ForeignKey(
                        name: "FK_SupplierOrderDetails_Products_sod_product_id",
                        column: x => x.sod_product_id,
                        principalTable: "Products",
                        principalColumn: "pro_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SupplierOrderDetails_SupplierOrders_sod_order_id",
                        column: x => x.sod_order_id,
                        principalTable: "SupplierOrders",
                        principalColumn: "so_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_CustomerCusId",
                table: "Addresses",
                column: "CustomerCusId");

            migrationBuilder.CreateIndex(
                name: "IX_AlcoholProducts_pro_type_id",
                table: "AlcoholProducts",
                column: "pro_type_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderDetails_cod_order_id",
                table: "CustomerOrderDetails",
                column: "cod_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderDetails_cod_product_id",
                table: "CustomerOrderDetails",
                column: "cod_product_id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_AddressAddId",
                table: "CustomerOrders",
                column: "AddressAddId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_co_address_id",
                table: "CustomerOrders",
                column: "co_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrders_co_customer_id",
                table: "CustomerOrders",
                column: "co_customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_cus_default_address_id",
                table: "Customers",
                column: "cus_default_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_pro_supplier_id",
                table: "Products",
                column: "pro_supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_pro_type_id",
                table: "Products",
                column: "pro_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrderDetails_sod_order_id",
                table: "SupplierOrderDetails",
                column: "sod_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrderDetails_sod_product_id",
                table: "SupplierOrderDetails",
                column: "sod_product_id");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrders_AddressAddId",
                table: "SupplierOrders",
                column: "AddressAddId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrders_so_address_id",
                table: "SupplierOrders",
                column: "so_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierOrders_so_supplier_id",
                table: "SupplierOrders",
                column: "so_supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_sup_default_address_id",
                table: "Suppliers",
                column: "sup_default_address_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Customers_CustomerCusId",
                table: "Addresses",
                column: "CustomerCusId",
                principalTable: "Customers",
                principalColumn: "cus_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Customers_CustomerCusId",
                table: "Addresses");

            migrationBuilder.DropTable(
                name: "AlcoholProducts");

            migrationBuilder.DropTable(
                name: "CustomerOrderDetails");

            migrationBuilder.DropTable(
                name: "SupplierOrderDetails");

            migrationBuilder.DropTable(
                name: "CustomerOrders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SupplierOrders");

            migrationBuilder.DropTable(
                name: "Types");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Addresses");
        }
    }
}
