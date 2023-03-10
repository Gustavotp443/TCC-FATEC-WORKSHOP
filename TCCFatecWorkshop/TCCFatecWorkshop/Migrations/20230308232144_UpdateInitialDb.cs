using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TCCFatecWorkshop.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInitialDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_products_services_tb_products_ProductId",
                table: "tb_products_services");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_products_services_tb_services_ServiceId",
                table: "tb_products_services");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_products_suppliers_tb_products_ProductId",
                table: "tb_products_suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_products_suppliers_tb_suppliers_SupplierId",
                table: "tb_products_suppliers");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "tb_workshops",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_products_services_tb_products_ServiceId",
                table: "tb_products_services",
                column: "ServiceId",
                principalTable: "tb_products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_products_services_tb_services_ProductId",
                table: "tb_products_services",
                column: "ProductId",
                principalTable: "tb_services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_products_suppliers_tb_products_SupplierId",
                table: "tb_products_suppliers",
                column: "SupplierId",
                principalTable: "tb_products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_products_suppliers_tb_suppliers_ProductId",
                table: "tb_products_suppliers",
                column: "ProductId",
                principalTable: "tb_suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_products_services_tb_products_ServiceId",
                table: "tb_products_services");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_products_services_tb_services_ProductId",
                table: "tb_products_services");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_products_suppliers_tb_products_SupplierId",
                table: "tb_products_suppliers");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_products_suppliers_tb_suppliers_ProductId",
                table: "tb_products_suppliers");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "tb_workshops",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_products_services_tb_products_ProductId",
                table: "tb_products_services",
                column: "ProductId",
                principalTable: "tb_products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_products_services_tb_services_ServiceId",
                table: "tb_products_services",
                column: "ServiceId",
                principalTable: "tb_services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_products_suppliers_tb_products_ProductId",
                table: "tb_products_suppliers",
                column: "ProductId",
                principalTable: "tb_products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_products_suppliers_tb_suppliers_SupplierId",
                table: "tb_products_suppliers",
                column: "SupplierId",
                principalTable: "tb_suppliers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
