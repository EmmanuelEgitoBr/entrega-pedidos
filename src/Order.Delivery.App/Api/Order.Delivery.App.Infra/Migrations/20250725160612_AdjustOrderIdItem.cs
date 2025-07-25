using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Order.Delivery.App.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AdjustOrderIdItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Orders_OrderId1",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_OrderId1",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "OrderId1",
                table: "Items");

            migrationBuilder.AlterColumn<string>(
                name: "OrderId",
                table: "Items",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Items_OrderId",
                table: "Items",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Orders_OrderId",
                table: "Items",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Orders_OrderId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_OrderId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderId1",
                table: "Items",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_OrderId1",
                table: "Items",
                column: "OrderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Orders_OrderId1",
                table: "Items",
                column: "OrderId1",
                principalTable: "Orders",
                principalColumn: "OrderId");
        }
    }
}
