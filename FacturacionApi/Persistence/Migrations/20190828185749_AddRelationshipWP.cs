using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.Migrations
{
    public partial class AddRelationshipWP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WarehouseId",
                table: "Product",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Product_WarehouseId",
                table: "Product",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Warehouse_WarehouseId",
                table: "Product",
                column: "WarehouseId",
                principalTable: "Warehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Warehouse_WarehouseId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_WarehouseId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "Product");
        }
    }
}
