using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketsApp.Data.Migrations
{
    public partial class QuantityCRUD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_ShoppingCart_ShoppingCartId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCart_AspNetUsers_OwnerId",
                table: "ShoppingCart");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketInShoppingCart_ShoppingCart_ShoppingCartId",
                table: "TicketInShoppingCart");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketInShoppingCart_Ticket_TicketId",
                table: "TicketInShoppingCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketInShoppingCart",
                table: "TicketInShoppingCart");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCart",
                table: "ShoppingCart");

            migrationBuilder.RenameTable(
                name: "TicketInShoppingCart",
                newName: "TicketInShoppingCarts");

            migrationBuilder.RenameTable(
                name: "ShoppingCart",
                newName: "ShoppingCarts");

            migrationBuilder.RenameIndex(
                name: "IX_TicketInShoppingCart_ShoppingCartId",
                table: "TicketInShoppingCarts",
                newName: "IX_TicketInShoppingCarts_ShoppingCartId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCart_OwnerId",
                table: "ShoppingCarts",
                newName: "IX_ShoppingCarts_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketInShoppingCarts",
                table: "TicketInShoppingCarts",
                columns: new[] { "TicketId", "ShoppingCartId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ShoppingCarts_ShoppingCartId",
                table: "Order",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_OwnerId",
                table: "ShoppingCarts",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketInShoppingCarts_ShoppingCarts_ShoppingCartId",
                table: "TicketInShoppingCarts",
                column: "ShoppingCartId",
                principalTable: "ShoppingCarts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketInShoppingCarts_Ticket_TicketId",
                table: "TicketInShoppingCarts",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_ShoppingCarts_ShoppingCartId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_AspNetUsers_OwnerId",
                table: "ShoppingCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketInShoppingCarts_ShoppingCarts_ShoppingCartId",
                table: "TicketInShoppingCarts");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketInShoppingCarts_Ticket_TicketId",
                table: "TicketInShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketInShoppingCarts",
                table: "TicketInShoppingCarts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingCarts",
                table: "ShoppingCarts");

            migrationBuilder.RenameTable(
                name: "TicketInShoppingCarts",
                newName: "TicketInShoppingCart");

            migrationBuilder.RenameTable(
                name: "ShoppingCarts",
                newName: "ShoppingCart");

            migrationBuilder.RenameIndex(
                name: "IX_TicketInShoppingCarts_ShoppingCartId",
                table: "TicketInShoppingCart",
                newName: "IX_TicketInShoppingCart_ShoppingCartId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingCarts_OwnerId",
                table: "ShoppingCart",
                newName: "IX_ShoppingCart_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketInShoppingCart",
                table: "TicketInShoppingCart",
                columns: new[] { "TicketId", "ShoppingCartId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingCart",
                table: "ShoppingCart",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ShoppingCart_ShoppingCartId",
                table: "Order",
                column: "ShoppingCartId",
                principalTable: "ShoppingCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCart_AspNetUsers_OwnerId",
                table: "ShoppingCart",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketInShoppingCart_ShoppingCart_ShoppingCartId",
                table: "TicketInShoppingCart",
                column: "ShoppingCartId",
                principalTable: "ShoppingCart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketInShoppingCart_Ticket_TicketId",
                table: "TicketInShoppingCart",
                column: "TicketId",
                principalTable: "Ticket",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
