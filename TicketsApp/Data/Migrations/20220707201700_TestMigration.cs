using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketsApp.Data.Migrations
{
    public partial class TestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketsInOrders_Orders_OrderId",
                table: "TicketsInOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketsInOrders_Tickets_TicketId",
                table: "TicketsInOrders");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketsInOrders_Tickets_OrderId",
                table: "TicketsInOrders",
                column: "OrderId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketsInOrders_Orders_TicketId",
                table: "TicketsInOrders",
                column: "TicketId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketsInOrders_Tickets_OrderId",
                table: "TicketsInOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketsInOrders_Orders_TicketId",
                table: "TicketsInOrders");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketsInOrders_Orders_OrderId",
                table: "TicketsInOrders",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketsInOrders_Tickets_TicketId",
                table: "TicketsInOrders",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
